using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_2 : Task_Base
{
    public Transform Task_2_Start_Position;
    public GameObject _XRController;

    public override IEnumerator Data_Set()
    {
        AudioClips = new List<AudioClip>()
        {
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_Tips_1"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_Tips_2"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_1"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_2"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_3"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_4"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_5"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_6"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_7"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_8"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_9"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_10"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_11"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_12"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_13"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_14"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3_15"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_Tips_1"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_Tips_2"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_Leave_1"),
        };

        UI_Contents = new List<string>()
        {
            GameManager.Instance.ControllerData.DataUI.GetData("Task_3_1_Title"),
            GameManager.Instance.ControllerData.DataUI.GetData("Task_3_1_Content"),
        };

        yield break;
    }

    /// <summary>
    /// 任务列表
    /// </summary>
    public override List<IEnumerator> TaskSequence => new List<IEnumerator>()
    {
        PlayStartPosition(),
        PlayStartShowUIAndRead(),
        WaitForCounter(),
    };

    //音频数据
    public List<AudioClip> AudioClips;

    //UI数据
    public List<string> UI_Contents;

    /// <summary>
    /// 场景二玩家位置初始化
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayStartPosition()
    {
        _XRController.transform.position = Task_2_Start_Position.position;

        yield break;
    }

    /// <summary>
    /// 进入场景and朗读
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayStartShowUIAndRead()
    {
        yield return StartCoroutine(GameManager.Instance.ControllerUI.UITask2TaskTips.SetTitel(UI_Contents[0]));
        yield return StartCoroutine(GameManager.Instance.ControllerUI.UITask2TaskTips.SetContent(UI_Contents[1]));
        yield return StartCoroutine(GameManager.Instance.ControllerUI.UITask2TaskTips.Fade(0f, 1f, 1f));
        yield return StartCoroutine(GameManager.Instance.ControllerTalk.TalkPlayer.PlayTalk(AudioClips[17]));
        yield return StartCoroutine(GameManager.Instance.ControllerTalk.TalkPlayer.PlayTalk(AudioClips[18]));
        Debug.Log("进入场景and朗读结束");
    }

    /// <summary>
    /// 进行柜台办理
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForCounter()
    {
        bool finishFlag = false;
        bool levelFlag = false;
        Coroutine enter = null;
        Coroutine exit = null;

        GameManager.Instance.ControllerProp.PropShowcaseCounter.EOnCollisionEnter.AddListener((other, showcase) =>
        {
            if (exit != null)
            {
                StopCoroutine(exit);
                exit = null;
            }

            enter = StartCoroutine(EnterShowCase());
        });
        GameManager.Instance.ControllerProp.PropShowcaseCounter.EOnCollisionExit.AddListener((other, showcase) =>
        {
            StopCoroutine(enter);
            enter = null;
            exit = StartCoroutine(LeaveShowCase());
        });

        yield return new WaitUntil(() => finishFlag);

        IEnumerator EnterShowCase()
        {
            Debug.Log("进入展柜");
            for (int i = 2; i <= 16; i++)
            {
                GameManager.Instance.ControllerNpc.NpcTask21.PlayAnimator("Talk_1");
                yield return StartCoroutine(GameManager.Instance.ControllerTalk.TalkPlayer.PlayTalk(AudioClips[i]));
            }

            finishFlag = true;
            yield break;
        }

        IEnumerator LeaveShowCase()
        {
            Debug.Log("离开展柜");
            // TODO: 处理离开柜台后的逻辑
            GameManager.Instance.ControllerNpc.NpcTask21.PlayAnimator("Leave");
            GameManager.Instance.ControllerTalk.TalkPlayer.PlayTalk(AudioClips[19]);
            yield return new WaitUntil(GameManager.Instance.ControllerNpc.NpcTask21.AnimationState);
            GameManager.Instance.ControllerNpc.NpcTask21.PlayAnimator(null);
        }

        yield break;
    }

    /// <summary>
    /// UI关闭状态的任务UI的读与写
    /// </summary>
    /// <param name="Content"></param>
    /// <param name="audioClip"></param>
    /// <returns></returns>
    IEnumerator TaskTipsReadAndWrite(string titel, string Content, AudioClip audioClip)
    {
        IEnumerator[] temp = new[]
        {
            GameManager.Instance.ControllerUI.UITask2TaskTips.Fade(0, 1, 1f),
            //设置标题
            GameManager.Instance.ControllerUI.UITask2TaskTips.SetTitel(titel),
            //设置文本
            GameManager.Instance.ControllerUI.UITask2TaskTips.SetContent(Content),
            //朗读
            GameManager.Instance.ControllerTalk.TalkPlayer.PlayTalk(audioClip),
        };

        yield return StartCoroutine(base.ConcurrentTake(temp));
    }
}