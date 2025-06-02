using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Platform
{
    public class H5Storage : IStorage
    {

        // 存储路径（自动适配各平台）
        private readonly string StoragePath = Path.Combine(Application.persistentDataPath, "localStorage.json");
        // 内存中的键值对缓存
        private Dictionary<string, string> _storageData = new();

        public H5Storage()
        {
#if UNITY_EDITOR
            if (File.Exists(StoragePath))
            {
                try
                {
                    string json = File.ReadAllText(StoragePath, Encoding.UTF8);
                    SerializableDictionary sd = JsonUtility.FromJson<SerializableDictionary>(json);
                    _storageData = sd.ToDictionary();
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"加载本地存储失败: {e.Message}");
                }
            }
#endif
        }

        public void SetString(string key, string value)
        {
            if (_storageData.ContainsKey(key))
                _storageData[key] = value;
            else
                _storageData.Add(key, value);

            SaveToDisk();
        }

        public void SetFloat(string key, float value)
        {
            SetString(key, value.ToString("G"));
        }

        public void SetInt(string key, int value)
        {
            SetString(key, value.ToString());
        }

        public void SetBool(string key, bool value)
        {
            SetString(key, value.ToString());
        }

        public string GetString(string key)
        {
            return _storageData.TryGetValue(key, out string value) ? value : null;
        }

        public int GetInt(string key)
        {
            string value = GetString(key);
            return int.TryParse(value, out int result) ? result : 0;
        }

        public float GetFloat(string key)
        {
            string value = GetString(key);
            return float.TryParse(value, out float result) ? result : 0f;
        }

        public bool GetBool(string key, bool defaultBool)
        {
            string value = GetString(key);
            return bool.TryParse(value, out bool result) ? result : defaultBool;
        }

        public void Remove(string key)
        {
            if (_storageData.ContainsKey(key))
            {
                _storageData.Remove(key);
                SaveToDisk();
            }
        }

        public void Clear()
        {
            _storageData.Clear();
            SaveToDisk();
        }

        /// <summary>
        /// 保存数据到磁盘
        /// </summary>
        private void SaveToDisk()
        {
            try
            {
                var sd = new SerializableDictionary();
                sd.Initialize(_storageData);
                string json = JsonUtility.ToJson(sd);
                File.WriteAllText(StoragePath, json, Encoding.UTF8);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"保存本地存储失败: {e.Message}");
            }
        }

    }

    [System.Serializable]
    public class SerializableDictionary
    {
        public List<string> keys = new();
        public List<string> values = new();

        public void Initialize(Dictionary<string, string> dict)
        {
            foreach (var pair in dict)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> dict = new();
            for (int i = 0; i < keys.Count; i++)
            {
                dict.Add(keys[i], values[i]);
            }
            return dict;
        }
    }

}
