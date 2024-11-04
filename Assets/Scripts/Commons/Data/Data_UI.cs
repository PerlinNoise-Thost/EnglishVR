using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_UI : Data_Base<Data_UI>
{
    //路径
    public override string path => "Assets/Resources/Form/UI_Text.xlsx";
    //数据的结构
    private class _Data
    {
        public string ID;
        public string Content;
    }
    //数据
    private Dictionary<string, _Data> dataSheet;
    //长度
    public int length = 2;
    
    /// <summary>
    /// 读取UI数据
    /// </summary>
    /// <returns></returns>
    public IEnumerator Load_Data_UI()
    {
        dataSheet = new Dictionary<string, _Data>();
        yield return StartCoroutine(LoadCFG(length, dataSheet));
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <param name="key">key</param>
    /// <returns></returns>
    public string GetData(string key) => dataSheet[key].Content;
}
