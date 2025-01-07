namespace Setting
{
    public enum LanguageType
    {
        /// <summary>
        /// 简体中文
        /// </summary>
        zh,
        /// <summary>
        /// 英语
        /// </summary>
        en,
        /// <summary>
        /// 日语
        /// </summary>
        ja,
        /// <summary>
        /// 韩语
        /// </summary>
        ko,
        /// <summary>
        /// 繁体中文
        /// </summary>
        tc,
        /// <summary>
        /// 西班牙语
        /// </summary>
        es,
        /// <summary>
        /// 法语
        /// </summary>
        fr,
        /// <summary>
        /// 德语
        /// </summary>
        de,
        /// <summary>
        /// 俄语
        /// </summary>
        ru,
        /// <summary>
        /// 葡萄牙语
        /// </summary>
        pt,
        /// <summary>
        /// 意大利语
        /// </summary>
        it,
        /// <summary>
        /// 越南语
        /// </summary>
        vn
    }

    public struct Language
    {
        public LanguageType lang;
    }

    public struct Audio
    {
        float volume;
        bool mute;
    }
}
