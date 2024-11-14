using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Task2_TaskTips : UI_Base
{
    public TextMeshProUGUI TMPTitel;
    public TextMeshProUGUI TMPContent;

    public override IEnumerator IniUI()
    {
        yield return StartCoroutine(base.IniUI());
        yield return StartCoroutine(base.ExitNowUI());
    }

    /// <summary>
    /// 设置标题
    /// </summary>
    /// <param name="uiData">标题的内容</param>
    /// <returns></returns>
    public IEnumerator SetTitel(string uiData)
    {
        yield return StartCoroutine(ChangeTitel(TMPTitel, uiData));
    }
    
    /// <summary>
    /// 设置主体值
    /// </summary>
    /// <param name="uiData"></param>
    /// <returns></returns>
    public IEnumerator SetContent(string uiData)
    {
        yield return StartCoroutine(ChangeTitel(TMPContent, uiData));
    }
}