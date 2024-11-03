using System.Collections;
using TMPro;
using UnityEngine;

public class UI_FacePanel_2 : UI_Base
{
    //标题
    public TextMeshProUGUI titel;
    //正文
    public TextMeshProUGUI mainText;
    
    protected override IEnumerator data_Set()
    {
        yield return null;
    }
    
    /// <summary>
    /// 关闭当前面板(渐隐)
    /// </summary>
    /// <returns></returns>
    public override IEnumerator ExitNowUI()
    {
        //渐隐
        yield return StartCoroutine(Fade(this.GetComponent<CanvasGroup>(), 1f, 0f, 1f));
        //关闭UI
        yield return StartCoroutine(base.ExitNowUI());
    }
    
    /// <summary>
    /// 打开当前面板(渐显)
    /// </summary>
    /// <returns></returns>
    public override IEnumerator OpenNowUI()
    {
        this.GetComponent<CanvasGroup>().alpha = 0;
        this.gameObject.SetActive(true);
        //渐显
        yield return StartCoroutine(Fade(this.GetComponent<CanvasGroup>(), 0f, 1f, 1f));
    }

    /// <summary>
    /// 改标题
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public IEnumerator ChangeTitel(string key)
    {
        yield return StartCoroutine(ChangeContent(titel,uiData.GetData(key)));
    }

    /// <summary>
    /// 改正文
    /// </summary>
    /// <param name="content">内容</param>
    /// <returns></returns>
    public IEnumerator ChangeContent(string key)
    {
        yield return StartCoroutine(ChangeContent(mainText, uiData.GetData(key)));
    }
}