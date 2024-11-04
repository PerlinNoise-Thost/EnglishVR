using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_1 : Task_Base
{
    /// <summary>
    /// 任务列表
    /// </summary>
    /// <returns></returns>
    public override List<IEnumerator> TaskSequence => new List<IEnumerator>()
    {
        ReadAndShowUI("Take_2_3", "Take_2_3"),
        WaitTime(1f),
        GameManager.Instance.ControllerUI.UIScreen.SetContent(null),
        WaitTime(1f),
        ReadAndShowUI("Take_2_4", "Take_2_4"),
        WaitTime(1f),
        GameManager.Instance.ControllerUI.UIScreen.SetContent(null),
        WaitTime(1f),
        ReadAndShowUI("Take_2_5", "Take_2_5"),
        WaitTime(1f),
        GameManager.Instance.ControllerUI.UIScreen.SetContent(null),
        WaitTime(1f),
    };

    /// <summary>
    /// 读取和显示
    /// </summary>
    /// <param name="TalkKey">TalkKey</param>
    /// <param name="UIKey">UIKey</param>
    /// <returns></returns>
    public IEnumerator ReadAndShowUI(string TalkKey, string UIKey)
    {
        var uiData = GameManager.Instance.ControllerData.DataUI.GetData(UIKey);
        var talkData = GameManager.Instance.ControllerData.DataTalk.GetData(TalkKey);

        IEnumerator[] Itemp = new IEnumerator[]
        {
            // 显示 TMP
            GameManager.Instance.ControllerUI.UIScreen.SetContent(uiData),
            // 朗读文本
            GameManager.Instance.ControllerTalk.TalkPlayer.PlayTalk(talkData)
        };
    
        yield return StartCoroutine(ConcurrentTake(Itemp));
    }


    /// <summary>
    /// 等待时间
    /// </summary>
    /// <param name="timer">时间</param>
    /// <returns></returns>
    public IEnumerator WaitTime(float timer)
    {
        yield return new WaitForSeconds(timer);
    }
}