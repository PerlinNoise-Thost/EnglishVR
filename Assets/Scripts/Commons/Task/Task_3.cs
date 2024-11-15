using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_3 : Task_Base
{
    public override IEnumerator Data_Set()
    {
        AudioClips = new List<AudioClip>()
        {
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3c_1"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3c_2"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3c_3"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3c_4"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3c_5"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3c_6"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3c_7"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3c_8"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3c_9"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3c_10"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3c_11"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3c_12"),
            
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3s_1"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3s_2"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3s_3"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3s_4"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3s_5"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3s_6"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3s_7"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Task_3s_8"),
        };
        
        yield break;
    }

    public List<AudioClip> AudioClips = null;

    public override List<IEnumerator> TaskSequence => new List<IEnumerator>()
    {
        
    };
    
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

        GameManager.Instance.ControllerProp.PropShowcaseCounterCustoms.EOnCollisionEnter.AddListener((other, showcase) =>
        {
            if (exit != null)
            {
                StopCoroutine(exit);
                exit = null;
            }

            enter = StartCoroutine(EnterShowCase());
        });
        GameManager.Instance.ControllerProp.PropShowcaseCounterCustoms.EOnCollisionExit.AddListener((other, showcase) =>
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
}