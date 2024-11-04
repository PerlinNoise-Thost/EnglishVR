using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class UI_Base : MonoBehaviour
{
    /// <summary>
    /// 初始化方法
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator IniUI()
    {
        if (this.GetComponent<CanvasGroup>() == null)
        {
            this.AddComponent<CanvasGroup>();
        }

        yield return null;
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
    /// 更改TMP内容
    /// </summary>
    /// <param name="TMP">TMP 引用</param>
    /// <param name="content">需要更换的内容</param>
    /// <returns></returns>
    public virtual IEnumerator ChangeTitel(TextMeshProUGUI TMP,string content)
    {
        TMP.text = content;
        yield return null;
    }
}