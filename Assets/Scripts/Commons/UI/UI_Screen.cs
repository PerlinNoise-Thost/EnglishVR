using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Screen : UI_Base
{
    //标题TMP
    public TextMeshProUGUI TMP_Titel;
    //正文
    public TextMeshProUGUI TMP_Content;

    /// <summary>
    /// 初始化方法
    /// </summary>
    /// <returns></returns>
    public override IEnumerator IniUI()
    {
        yield return StartCoroutine(base.IniUI());
        var titel = GameManager.Instance.ControllerData.DataUI.GetData("M_1_LeadIn_Title");
        var _content = GameManager.Instance.ControllerData.DataUI.GetData("Take_2_7");
        yield return StartCoroutine(SetTitel(titel));
        yield return StartCoroutine(SetContent(_content));
    }
    
    /// <summary>
    /// 设置标题
    /// </summary>
    /// <param name="content">内容</param>
    /// <returns></returns>
    public IEnumerator SetTitel(string content)
    {
        yield return StartCoroutine(ChangeTitel(TMP_Titel, content));
    }  
    
    /// <summary>
    /// 设置正文
    /// </summary>
    /// <param name="content">内容</param>
    /// <returns></returns>
    public IEnumerator SetContent(string content)
    {
        yield return StartCoroutine(ChangeTitel(TMP_Content, content));
    }

}