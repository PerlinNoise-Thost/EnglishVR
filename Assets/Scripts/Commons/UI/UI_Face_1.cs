using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Face_1 : UI_Base
{
    //主要内容
    public TextMeshProUGUI TMP_Content;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    public override IEnumerator IniUI()
    {
       yield return StartCoroutine(base.IniUI());
       TMP_Content.text = null;
       //隐藏
       yield return ExitNowUI();
    }

    /// <summary>
    /// 设置主体值
    /// </summary>
    /// <param name="uiData"></param>
    /// <returns></returns>
    public IEnumerator SetContent(string uiData)
    {
        yield return StartCoroutine(ChangeTitel(TMP_Content, uiData));
    }
}
