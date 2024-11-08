using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_ButtonTask_1 : UI_Base
{
    public Button enter_Level_1;

    public override IEnumerator IniUI()
    {
        yield return StartCoroutine(base.IniUI());
        yield return StartCoroutine(base.ExitNowUI());
    }

    /// <summary>
    /// 打开按钮
    /// </summary>
    /// <param name="_alpha"></param>
    /// <returns></returns>
    public override IEnumerator OpenNowUI(float _alpha)
    {
        yield return StartCoroutine(base.OpenNowUI(0));
        yield return StartCoroutine(Fade(0, 1, 1));
    }

    /// <summary>
    /// 关闭按钮
    /// </summary>
    /// <returns></returns>
    public override IEnumerator ExitNowUI()
    {
        yield return StartCoroutine(Fade(1, 0, 1));
        yield return StartCoroutine(base.ExitNowUI());
    }

    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="arg">事件</param>
    /// <returns></returns>
    public IEnumerator OnClickAddListener(UnityAction arg)
    {
        enter_Level_1.onClick.AddListener(arg);
        yield break;
    }

    public IEnumerator OnClickRemoveListener(UnityAction arg)
    {
        enter_Level_1.onClick.RemoveListener(arg);
        yield break;
    }
}