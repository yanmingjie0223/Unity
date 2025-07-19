#if !UNITY_EDITOR && DOUYINMINIGAME
using System.Collections.Generic;
using System;
using TTSDK;

namespace Assets.Scripts.Platform
{
    public class DYStorage : IStorage
    {
        private TTSaveData data;

        public void Clear()
        {
            TT.DeleteSaving<TTSaveData>(GameConfig.appName);
        }

        public void Remove(string key)
        {
            Load();
            if (data.stringValueMap.ContainsKey(key))
            {
                data.stringValueMap.Remove(key);
            }
            if (data.intValueMap.ContainsKey(key))
            {
                data.intValueMap.Remove(key);
            }
            if (data.floatValueMap.ContainsKey(key))
            {
                data.floatValueMap.Remove(key);
            }
            if (data.boolValueMap.ContainsKey(key))
            {
                data.boolValueMap.Remove(key);
            }
        }

        public int GetInt(string key)
        {
            Load();
            if (data.intValueMap.TryGetValue(key, out int value))
            {
                return value;
            }
            else
            {
                return 0;
            }
        }

        public float GetFloat(string key)
        {
            Load();
            if (data.floatValueMap.TryGetValue(key, out float value))
            {
                return value;
            }
            else
            {
                return 0f;
            }
        }

        public string GetString(string key)
        {
            Load();
            if (data.stringValueMap.TryGetValue(key, out string value))
            {
                return value;
            }
            else
            {
                return "";
            }
        }

        public bool GetBool(string key, bool defaultBool)
        {
            Load();
            if (data.boolValueMap.TryGetValue(key, out bool value))
            {
                return value;
            }
            else
            {
                return defaultBool;
            }
        }

        public void SetFloat(string key, float value)
        {
            Load();
            if (data.floatValueMap.ContainsKey(key))
            {
                data.floatValueMap[key] = value;
            }
            else
            {
                data.floatValueMap.Add(key, value);
            }
            TT.Save(data, GameConfig.appName);
        }

        public void SetInt(string key, int value)
        {
            Load();
            if (data.intValueMap.ContainsKey(key))
            {
                data.intValueMap[key] = value;
            }
            else
            {
                data.intValueMap.Add(key, value);
            }
            TT.Save(data, GameConfig.appName);
        }

        public void SetString(string key, string value)
        {
            Load();
            if (string.IsNullOrEmpty(value))
            {
                data.stringValueMap.Remove(key);
            }
            else
            {
                if (data.stringValueMap.ContainsKey(key))
                {
                    data.stringValueMap[key] = value;
                }
                else
                {
                    data.stringValueMap.Add(key, value);
                }
            }
            TT.Save(data, GameConfig.appName);
        }

        public void SetBool(string key, bool value)
        {
            Load();
            if (data.boolValueMap.ContainsKey(key))
            {
                data.boolValueMap[key] = value;
            }
            else
            {
                data.boolValueMap.Add(key, value);
            }
            TT.Save(data, GameConfig.appName);
        }

        private void Load()
        {
            if (data != null)
            {
                return;
            }
            data = TT.LoadSaving<TTSaveData>(GameConfig.appName);
            if (data == null)
            {
                data = new TTSaveData();
                data.Initialize();
            }
        }
    }
}

[Serializable]
class TTSaveData
{
    public Dictionary<string, bool> boolValueMap;
    public Dictionary<string, int> intValueMap;
    public Dictionary<string, float> floatValueMap;
    public Dictionary<string, string> stringValueMap;

    public void Initialize()
    {
        if (boolValueMap == null)
        {
            boolValueMap = new Dictionary<string, bool>();
        }
        if (intValueMap == null)
        {
            intValueMap = new Dictionary<string, int>();
        }
        if (floatValueMap == null)
        {
            floatValueMap = new Dictionary<string, float>();
        }
        if (stringValueMap == null)
        {
            stringValueMap = new Dictionary<string, string>();
        }
    }
}
#endif