using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Talk : Data_Base
{
    //数据的结构
    private class _Data
    {
        public string ID;
        public string NPC;
        public string Content;
        public AudioClip audio;
    }
    //字典
    private Dictionary<string, _Data> dataSheet;
    
    //路径
    public override string path => "Assets/Resources/Form/Talk.xlsx";
    //长度
    public int length = 3;
    
    /// <summary>
    /// 读取表格
    /// </summary>
    /// <param name="content">字典</param>
    /// <typeparam name="T">类</typeparam>
    /// <returns></returns>
    public IEnumerator Load_Data_Talk<T>(Dictionary<string,T> content) where T : new()
    {
        dataSheet = new Dictionary<string, _Data>();
        yield return StartCoroutine(LoadCFG(path, length, dataSheet));
    }

    public IEnumerator GetData()
    {
        
    }
}
