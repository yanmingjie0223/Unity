
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
public partial class Tables
{
    public ncb.TbItem TbItem {get; }
    public ncb.TbGlobal TbGlobal {get; }
    public ncb.TbLanguage TbLanguage {get; }

    public Tables(System.Func<string, ByteBuf> loader)
    {
        TbItem = new ncb.TbItem(loader("ncb_tbitem"));
        TbGlobal = new ncb.TbGlobal(loader("ncb_tbglobal"));
        TbLanguage = new ncb.TbLanguage(loader("ncb_tblanguage"));
        ResolveRef();
    }
    
    private void ResolveRef()
    {
        TbItem.ResolveRef(this);
        TbGlobal.ResolveRef(this);
        TbLanguage.ResolveRef(this);
    }
}

}
