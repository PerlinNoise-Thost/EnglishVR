using System.Collections;
using UnityEngine;

public class UI_FacePanel_1 : UI_Base
{
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
}