using System;
using System.Collections;
using System.IO;
using cfg;
using Luban;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LubanCfgTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void LubanCfgTestSimplePasses()
    {
        var tablesCtor = typeof(Tables).GetConstructors()[0];
        var loaderReturnType = tablesCtor.GetParameters()[0].ParameterType.GetGenericArguments()[1];
        Delegate loader = new Func<string, ByteBuf>(LoadByteBuf);
        var tables = (Tables)tablesCtor.Invoke(new object[] { loader });
        tables.TbItem.DataMap.TryGetValue(10000, out Item item);
        Debug.Log(item.ToString());
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator LubanCfgTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    private ByteBuf LoadByteBuf(string file)
    {
        return new ByteBuf(File.ReadAllBytes($"Assets/DynamicAssets/Luban/Datas/{file}.bytes"));
    }
}
