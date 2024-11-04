using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Data : Controller_Base, IInitialization
{
    #region 实现 IInitialization

    public string DataSetSequence => "Data_All";
    public IEnumerator Data_Set()
    {
        if (DataUI == null || DataTalk == null)
        {
            Debug.Log("数据类初始化缺少引用");
            yield break;
        }
        
        yield return StartCoroutine(DataTalk.Load_Data_Talk());
        yield return StartCoroutine(DataUI.Load_Data_UI());
    }

    #endregion
    
    [Header("初始化排序")]
    public List<String> Data_Set_Key = new List<string>()
    {
        "Data_All"
    };

    public Data_UI DataUI;
    public Data_Talk DataTalk;
}
