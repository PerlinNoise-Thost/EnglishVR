using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Data : Controller_Base, IInitialization
{
    #region 实现 IInitialization接口

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

    public void Awake()
    {
        StartCoroutine(InitializePool());
    }

    /// <summary>
    /// 初始化池
    /// </summary>
    /// <returns></returns>
    public IEnumerator InitializePool()
    {
        //实现IInitialization接口的实例列表
        List<IInitialization> initializations = null;
        //查找实例并赋值
        yield return StartCoroutine(base.FindInterfaces<IInitialization>((tempData) => initializations = tempData));
        //按照Data_Set_Key排序
        yield return StartCoroutine(base.SortByReference<IInitialization>(initializations, "DataSetSequence", Data_Set_Key));
        //遍历，初始化
        foreach (var i in initializations)
        {
            yield return StartCoroutine(i.DataSetSequence);
        }
        Debug.Log("初始化完成");
    }
}
