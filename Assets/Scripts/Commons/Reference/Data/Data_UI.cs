using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using UnityEngine;
public class Data_UI : Data_Base,IInitialization
{
    //数据类
    public class UI_Data
    {
        public string ID;
        public string Content;
    }

    //字典数据
    private Dictionary<string, UI_Data> UI_CFG = new Dictionary<string, UI_Data>();

    //Excel路径
    public string Excel_Path = "Assets/UI_Text.xlsx";

    //初始化用
    public override IEnumerator _data_Set()
    {
        yield return StartCoroutine(LoadCFG(Excel_Path,2,UI_CFG));
        Debug.Log("UI数据初始化成功");
    }

    //获取数据
    public override string GetData(string key)
    {
        return UI_CFG[key].Content;
    }
}