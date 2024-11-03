using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using TMPro;
using UnityEngine;

public abstract class UI_Base : MonoBehaviour,IInitialization
{
    //挂载Canvas Group的UI
    public GameObject thisPanel;
    //自定义初始化
    protected abstract IEnumerator data_Set();
    //初始化接口排序属性
    public virtual string DataSetSequence => GetType().Name; 

    //数据脚本
    protected Data_UI uiData = null;
    
    //实现初始化接口
    public IEnumerator Data_Set()
    {
        uiData = CenterManager.Instance._Data.Data_UI;
        
        yield return StartCoroutine(data_Set());
    }

    /// <summary>
    /// 打开
    /// </summary>
    public virtual IEnumerator OpenNowUI()
    {
        CanvasGroup panelCanvasGroup= GetComponent<CanvasGroup>();
        panelCanvasGroup.blocksRaycasts = true;
        panelCanvasGroup.alpha = 1;
        this.gameObject.SetActive(true);
        
        yield break;
    }
    
    /// <summary>
    /// 关闭
    /// </summary>
    public virtual IEnumerator ExitNowUI()
    {
        CanvasGroup panelCanvasGroup= GetComponent<CanvasGroup>();
        panelCanvasGroup.blocksRaycasts = false;
        panelCanvasGroup.alpha = 0;
        this.gameObject.SetActive(false);
        
        yield break;
    }
    
    /// <summary>
    /// CanvasGroup控制渐显渐隐
    /// Alpha 1:不透明 0:透明
    /// </summary>
    /// <param name="canvasGroup">CanvasGroup</param>
    /// <param name="startAlpha">起始Alpha</param>
    /// <param name="endAlpha">目标Alpha</param>
    /// <param name="time">时间</param>
    /// <returns></returns>
    protected IEnumerator Fade(CanvasGroup canvasGroup,float startAlpha, float endAlpha, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += UnityEngine.Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / time);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }

    /// <summary>
    /// 更改text内容
    /// </summary>
    /// <param name="tmp">tmp</param>
    /// <param name="content">内容</param>
    /// <returns></returns>
    public virtual IEnumerator ChangeContent(TextMeshProUGUI tmp, string content)
    {
        tmp.text = content;
        
        yield break;
    }
}
