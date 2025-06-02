namespace Assets.Scripts.Platform
{

    public interface IStorage
    {

        /// <summary>
        /// 存储数据到本地
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetString(string key, string value);
        void SetFloat(string key, float value);
        void SetInt(string key, int value);
        void SetBool(string key, bool value);

        /// <summary>
        /// 获取本地存储的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetString(string key);
        float GetFloat(string key);
        int GetInt(string key);
        bool GetBool(string key, bool defaultBool = false);

        /// <summary>
        /// 从本地存储中移除数据
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// 清除本地存储的数据
        /// </summary>
        void Clear();

    }

}
