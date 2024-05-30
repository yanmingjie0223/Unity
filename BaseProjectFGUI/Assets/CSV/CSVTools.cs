using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Flame.CSV
{
    public interface ITableStruct
    {
        public int GID { get; }
    }

    public interface ITable
    {
        public void Load();
    }

    public class CVSTableLoader
    {
        public static Dictionary<string, byte[]> ConfigSource = new();

        public static IEnumerator Preload(string bundleName, Action<string> start, Action<float> progress, Action<bool> end)
        {
            AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync(bundleName);
            start?.Invoke("loading start");
            while (handle.PercentComplete < 1 && !handle.IsDone)
            {
                progress?.Invoke(handle.PercentComplete);
                yield return null;
            }
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                bool isError = false;
                var length = handle.Result.Count;
                for (int i = 0; i < length; i++)
                {
                    var res = handle.Result[i];
                    var key = res.PrimaryKey;
                    AsyncOperationHandle<TextAsset> assetHandle = Addressables.LoadAssetAsync<TextAsset>(key);
                    start("load: " + key);
                    yield return assetHandle;
                    progress?.Invoke(((float)i + 1) / length);
                    if (assetHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        var txt = assetHandle.Result;
                        var n = Path.GetFileNameWithoutExtension(txt.name);
                        if (!ConfigSource.ContainsKey($"{n}"))
                        {
                            ConfigSource.Add($"{n}", txt.bytes);
                        }
                    }
                    else
                    {
                        isError = true;
                        break;
                    }
                }
                end?.Invoke(isError);
            }
            else
            {
                end?.Invoke(true);
            }
            Addressables.Release(handle);
        }

        public static List<List<string>> GetCsvData(string csvName)
        {
            List<List<string>> csvData = new();

            if (!ConfigSource.TryGetValue(csvName, out byte[] source))
            {
                return csvData;
            }

            MemoryStream tableStream = new(source);
            using var reader = new StreamReader(tableStream, Encoding.GetEncoding(936));

            var tempSB = new StringBuilder();
            var lineStr = reader.ReadLine();
            while (lineStr != null)
            {
                csvData.Add(ParseLine(lineStr, tempSB));
                lineStr = reader.ReadLine();
            }
            reader.Close();

            Release(csvName);

            return csvData;
        }

        public static List<string> ParseLine(string line, StringBuilder _columnBuilder)
        {
            List<string> Fields = new();
            bool inColumn = false;
            bool inQuotes = false;
            bool isNotEnd = false;
            _columnBuilder.Clear();

            // Iterate through every character in the line
            for (int i = 0; i < line.Length; i++)
            {
                char character = line[i];

                // If we are not currently inside a column
                if (!inColumn)
                {
                    // If the current character is a double quote then the column value is contained within
                    // double quotes, otherwise append the next character
                    inColumn = true;
                    if (character == '"')
                    {
                        inQuotes = true;
                        continue;
                    }

                }

                // If we are in between double quotes
                if (inQuotes)
                {
                    if ((i + 1) == line.Length)
                    {
                        if (character == '"')
                        {
                            inQuotes = false;
                            continue;
                        }
                        else
                        {
                            isNotEnd = true;
                        }
                    }
                    else if (character == '"' && line[i + 1] == ',')
                    {
                        inQuotes = false;
                        inColumn = false;
                        i++;
                    }
                    else if (character == '"' && line[i + 1] == '"')
                    {
                        i++;
                        if (line.Length - 1 == i)
                        {
                            isNotEnd = true;
                        }
                    }
                    else if (character == '"')
                    {
                        throw new Exception(string.Format("[{0}]:格式错误，错误的双引号转义 near [{1}] ", "ParseLine", line));
                    }

                }
                else if (character == ',')
                    inColumn = false;

                // If we are no longer in the column clear the builder and add the columns to the list
                if (!inColumn)
                {
                    Fields.Add(_columnBuilder.ToString());
                    _columnBuilder.Clear();

                }
                else // append the current column
                    _columnBuilder.Append(character);
            }

            // If we are still inside a column add a new one
            if (inColumn)
            {
                if (isNotEnd)
                {
                    _columnBuilder.Append("\r\n");
                }
                Fields.Add(_columnBuilder.ToString());
            }
            else
            {
                Fields.Add("");
            }

            return Fields;
        }

        public static void Release(string csvName)
        {
            if (ConfigSource.ContainsKey(csvName))
            {
                ConfigSource.Remove(csvName);
            }
        }

        public static void Release()
        {
            ConfigSource.Clear();
        }
    }

    public abstract class CSVTable<U, T> : ITable where U : ITableStruct, new()
    {
        private readonly Dictionary<int, U> _cache = new();

        public void Load()
        {
            var t = typeof(U);
            MemoryStream tableStream = new(CVSTableLoader.ConfigSource[t.Name]);
            using var reader = new StreamReader(tableStream, Encoding.GetEncoding(936));

            // 表说明、字段说明、字段类型
            reader.ReadLine();
            reader.ReadLine();
            reader.ReadLine();

            var fieldNameStr = reader.ReadLine();
            var tempSB = new StringBuilder();
            var fieldNameArray = CVSTableLoader.ParseLine(fieldNameStr, tempSB);
            List<FieldInfo> allFieldInfo = new();
            foreach (var fieldName in fieldNameArray)
            {
                allFieldInfo.Add(typeof(U).GetField(fieldName));
            }

            var lineStr = reader.ReadLine();
            while (lineStr != null)
            {
                var itemStrArray = CVSTableLoader.ParseLine(lineStr, tempSB);
                U DataDB = ReadLine(allFieldInfo, itemStrArray);
                _cache[DataDB.GID] = DataDB;
                lineStr = reader.ReadLine();
            }
            reader.Close();
        }

        /// <summary>
        /// 获取表格数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public U this[int index]
        {
            get
            {
                _cache.TryGetValue(index, out U db);
                return db;
            }
        }

        /// <summary>
        /// 得到整张表
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, U> GetAll()
        {
            return _cache;
        }

        /// <summary>
        /// 子类自行实现复杂类型的读取
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        protected virtual void ReadType(Type type, U data, FieldInfo field)
        {

        }

        /// <summary>
        /// 读取每行的数据 
        /// </summary>
        /// <param name="allFieldInfo">每条属性对应的类型列表</param>
        /// <param name="itemStrArray">一条分割好的数据</param>
        private U ReadLine(List<FieldInfo> allFieldInfo, List<string> itemStrArray)
        {
            var dataDB = new U();
            //对每个字段解析
            for (int i = 0; i < allFieldInfo.Count; ++i)
            {
                var field = allFieldInfo[i];
                var data = itemStrArray[i];
                if (field.FieldType == typeof(int))
                {
                    field.SetValue(dataDB, int.Parse(data));
                }
                if (field.FieldType == typeof(long))
                {
                    field.SetValue(dataDB, long.Parse(data));
                }
                else if (field.FieldType == typeof(string))
                {
                    field.SetValue(dataDB, data);
                }
                else if (field.FieldType == typeof(float))
                {
                    field.SetValue(dataDB, float.Parse(data));
                }
                else if (field.FieldType == typeof(bool))
                {
                    var curData = data.ToLower();
                    if (curData == "true" || curData == "1")
                    {
                        field.SetValue(dataDB, true);
                    }
                    else
                    {
                        field.SetValue(dataDB, false);
                    }
                }
                else if (field.FieldType == typeof(List<int>))
                {
                    var list = new List<int>();
                    foreach (var itemStr in data.Split('$'))
                    {
                        list.Add(int.Parse(itemStr));
                    }
                    field.SetValue(dataDB, list);
                }
                else if (field.FieldType == typeof(List<long>))
                {
                    var list = new List<long>();
                    foreach (var itemStr in data.Split('$'))
                    {
                        list.Add(long.Parse(itemStr));
                    }
                    field.SetValue(dataDB, list);
                }
                else if (field.FieldType == typeof(List<bool>))
                {
                    var list = new List<bool>();
                    foreach (var itemStr in data.Split('$'))
                    {
                        var curData = itemStr.ToLower();
                        if (curData == "true" || curData == "1")
                        {
                            list.Add(true);
                        }
                        else
                        {
                            list.Add(false);
                        }
                    }
                    field.SetValue(dataDB, list);
                }
                else if (field.FieldType == typeof(List<float>))
                {
                    var list = new List<float>();
                    foreach (var itemStr in data.Split('$'))
                    {
                        list.Add(float.Parse(itemStr));
                    }
                    field.SetValue(dataDB, list);
                }
                else if (field.FieldType == typeof(List<string>))
                {
                    field.SetValue(dataDB, new List<string>(data.Split('$')));
                }
                else if (field.FieldType == typeof(Type))
                {
                    Type type = field.FieldType.GetType();
                    ReadType(type, dataDB, field);
                }
            }

            return dataDB;
        }
    }
}
