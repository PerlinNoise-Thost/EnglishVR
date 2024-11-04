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
        yield return StartCoroutine(DataTalk.Load_Data_Talk());
        yield return StartCoroutine(DataUI.Load_Data_UI());
    }

    #endregion
    
    public Data_UI DataUI;
    public Data_Talk DataTalk;

   
}
