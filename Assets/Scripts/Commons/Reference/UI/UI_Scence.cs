using System.Collections;
using System.Collections.Generic;
using NAudio.Gui;
using TMPro;
using UnityEngine;

public class UI_Scence : UI_Base
{
    //屏幕显示的字体
    public TextMeshProUGUI mainTMP;
    
    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator data_Set()
    {
        yield break;
    }

    /// <summary>
    /// 显示TMP
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="keyCode">索引key</param>
    /// <returns></returns>
    public IEnumerator ShowTMP(float time,string keyCode)
    {
        mainTMP.text = uiData.GetData(keyCode);
        CanvasGroup tempCG = thisPanel.GetComponent<CanvasGroup>();
        yield return StartCoroutine(Fade(tempCG, 0f, 1f, time));
    }
    
    /// <summary>
    /// 隐藏TMP
    /// </summary>
    /// <param name="time">时间</param>
    /// <returns></returns>
    public IEnumerator HideTMP(float time)
    {
        CanvasGroup tempCG = thisPanel.GetComponent<CanvasGroup>();
        yield return StartCoroutine(Fade(tempCG, 1f, 0f, time));
    }
}
