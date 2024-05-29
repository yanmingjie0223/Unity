using System.Collections.Generic;

/// <summary>
/// 本地化表
/// <summary>
public class Language : IBaseTable
{
    public static string csv = "Language";
    /// <summary>
    /// id
    /// <summary>
    public int id;
    /// <summary>
    /// 语言
    /// <summary>
    public string key;
    /// <summary>
    /// 简体中文
    /// <summary>
    public string zh;
    /// <summary>
    /// 英文
    /// <summary>
    public string en;
    /// <summary>
    /// 日语
    /// <summary>
    public string ja;
    /// <summary>
    /// 韩语
    /// <summary>
    public string ko;
    /// <summary>
    /// 繁体中文
    /// <summary>
    public string tc;
    /// <summary>
    /// 西班牙语
    /// <summary>
    public string es;
    /// <summary>
    /// 法语
    /// <summary>
    public string fr;
    /// <summary>
    /// 德语
    /// <summary>
    public string de;
    /// <summary>
    /// 俄语
    /// <summary>
    public string ru;
    /// <summary>
    /// 葡萄牙语
    /// <summary>
    public string pt;
    /// <summary>
    /// 意大利语
    /// <summary>
    public string it;
    /// <summary>
    /// 越南语
    /// <summary>
    public string vn;

    public int GID 
    {
        get => id;
    }

}
public class LanguageTable : CSVConfigTable<Language, LanguageTable>
{

}
