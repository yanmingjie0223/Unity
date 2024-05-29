using System.Collections.Generic;

/// <summary>
/// 配置测试
/// <summary>
public class Test : IBaseTable
{
    public static string csv = "Test";
    /// <summary>
    /// 唯一id,"
    /// <summary>
    public int id;
    /// <summary>
    /// "个别数据"
    /// <summary>
    public List<int> data;
    /// <summary>
    /// 描述
    /// <summary>
    public string des;
    /// <summary>
    /// 字符串数组
    /// <summary>
    public List<string> cotents;
    /// <summary>
    /// 浮点型
    /// <summary>
    public float vec;
    /// <summary>
    /// 浮点型数组
    /// <summary>
    public List<float> vecs;
    /// <summary>
    /// 布尔
    /// <summary>
    public bool isShow;
    /// <summary>
    /// 布尔数组
    /// <summary>
    public List<bool> isShows;
    /// <summary>
    /// long类型
    /// <summary>
    public long argL;
    /// <summary>
    /// long类型数组
    /// <summary>
    public List<long> argLs;
    /// <summary>
    /// int64类型和long一样
    /// <summary>
    public long argI;
    /// <summary>
    /// int64类型数组
    /// <summary>
    public List<long> argIs;

    public int GID 
    {
        get => id;
    }

}
public class TestTable : CSVConfigTable<Test, TestTable>
{

}
