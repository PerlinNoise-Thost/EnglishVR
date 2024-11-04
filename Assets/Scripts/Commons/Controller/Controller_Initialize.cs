using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Initialize : Controller_Base
{
    public void Awake()
    {
        StartCoroutine(InitializePool());
    }
    
    [Header("初始化排序")]
    public List<String> Data_Set_Key = new List<string>()
    {
        "Data_All",
        "UI_All",
    };

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
            yield return StartCoroutine(i.Data_Set());
        }
        
    }
}