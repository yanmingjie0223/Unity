
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;


namespace cfg
{
public sealed partial class Global : Luban.BeanBase
{
    public Global(ByteBuf _buf) 
    {
        Key = _buf.ReadString();
        Content = _buf.ReadString();
    }

    public static Global DeserializeGlobal(ByteBuf _buf)
    {
        return new Global(_buf);
    }

    /// <summary>
    /// key
    /// </summary>
    public readonly string Key;
    /// <summary>
    /// 内容
    /// </summary>
    public readonly string Content;
   
    public const int __ID__ = 2135814083;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "key:" + Key + ","
        + "content:" + Content + ","
        + "}";
    }
}

}
