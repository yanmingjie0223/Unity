
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;


namespace cfg.ncb
{
public partial class TbItem
{
    private readonly System.Collections.Generic.Dictionary<int, ncb.Item> _dataMap;
    private readonly System.Collections.Generic.List<ncb.Item> _dataList;
    
    public TbItem(ByteBuf _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, ncb.Item>();
        _dataList = new System.Collections.Generic.List<ncb.Item>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            ncb.Item _v;
            _v = global::cfg.ncb.Item.DeserializeItem(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<int, ncb.Item> DataMap => _dataMap;
    public System.Collections.Generic.List<ncb.Item> DataList => _dataList;

    public ncb.Item GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public ncb.Item Get(int key) => _dataMap[key];
    public ncb.Item this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}

