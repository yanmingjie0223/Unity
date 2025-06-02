#if !UNITY_EDITOR && WEIXINMINIGAME
using WeChatWASM;

namespace Assets.Scripts.Platform
{

    public class WXStorage : IStorage
    {

        public void Clear()
        {
            WX.StorageDeleteAllSync();
        }

        public int GetInt(string key)
        {
            return WX.StorageGetIntSync(key, 0);
        }

        public float GetFloat(string key)
        {
            return WX.StorageGetFloatSync(key, 0);
        }

        public string GetString(string key)
        {
            return WX.StorageGetStringSync(key, "");
        }

        public bool GetBool(string key, bool defaultBool)
        {
            string value = WX.StorageGetStringSync(key, "");
            return bool.TryParse(value, out bool result) ? result : defaultBool;
        }

        public void Remove(string key)
        {
            WX.StorageDeleteKeySync(key);
        }

        public void SetString(string key, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                WX.StorageDeleteKeySync(key);
            }
            else
            {
                WX.StorageSetStringSync(key, value);
            }
        }

        public void SetFloat(string key, float value)
        {
            WX.StorageSetFloatSync(key, value);
        }

        public void SetInt(string key, int value)
        {
            WX.StorageSetIntSync(key, value);
        }

        public void SetBool(string key, bool value)
        {
            WX.StorageSetStringSync(key, value.ToString());
        }

    }

}
#endif
