using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_1 : Task_Base
{
    //Screen面板标题
    private string uiData_Screen_Title;
    //Screen面板正文_1
    private string uiData_Screen_Content_1;
    //Screen面板正文_2
    private string uiData_Screen_Content_2;
    
    public override IEnumerator Data_Set()
    {
        uiData_Screen_Title = GameManager.Instance.ControllerData.DataUI.GetData("Take_2_6");
        uiData_Screen_Content_1 = GameManager.Instance.ControllerData.DataUI.GetData("Take_2_7");
        uiData_Screen_Content_2 = GameManager.Instance.ControllerData.DataUI.GetData("Take_2_9");

        yield return null;
    }
    
    /// <summary>
    /// 任务列表
    /// </summary>
    /// <returns></returns>
    public override List<IEnumerator> TaskSequence => new List<IEnumerator>()
    {
        WaitTime(1f),
        
        GameManager.Instance.ControllerUI.UIScreen.SetTitel(uiData_Screen_Title),
        GameManager.Instance.ControllerUI.UIScreen.SetContent(uiData_Screen_Content_1),
        
        WaitTime(1f),
        
        GameManager.Instance.ControllerUI.UIFace1.OpenNowUI(0f),    //透明打开
        ReadAndShowUI("Take_2_3", "Take_2_3"),
        GameManager.Instance.ControllerUI.UIFace1.Fade(1f,0f,1f),   //渐隐
        WaitTime(1f),
        ReadAndShowUI("Take_2_4", "Take_2_4"),
        GameManager.Instance.ControllerUI.UIFace1.Fade(1f,0f,1f),   //渐隐
        WaitTime(1f),
        ReadAndShowUI("Take_2_5", "Take_2_5"),
        GameManager.Instance.ControllerUI.UIFace1.Fade(1f,0f,1f),   //渐隐
        WaitTime(3f),
        ReadAndShowUI("Take_2_8", "Take_2_8"),
        WaitTime(5f),
        GameManager.Instance.ControllerUI.UIFace1.Fade(1f,0f,1f),   //渐隐
        GameManager.Instance.ControllerUI.UIFace1.ExitNowUI(),  //关闭 Face_1 面板
        
        /* 教师导入部分完成 */
        GameManager.Instance.ControllerUI.UIScreen.Fade(1f,0f,1f),
        GameManager.Instance.ControllerUI.UIScreen.SetContent(uiData_Screen_Content_2),
        GameManager.Instance.ControllerUI.UIScreen.Fade(0f,1f,1f),
        
    };

    /// <summary>
    /// 读取和渐显
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
            //TMP 赋值
            GameManager.Instance.ControllerUI.UIFace1.SetContent(uiData),
            //渐显
            GameManager.Instance.ControllerUI.UIFace1.Fade(0f,1f,1f),
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