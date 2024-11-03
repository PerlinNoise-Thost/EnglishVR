using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class Task_2 : Task_Base
{
    //当前任务索引 ：2
    public override int CurrentTaskIndex() => 2;
    //任务列表
    public override List<IEnumerator> TaskSequence() => new List<IEnumerator>()
    {
        WaitTime(3f), //等待三秒
        _centerManager._ItemFacePanel.CameraOut(),  //面板出现
        _centerManager._ItemFacePanel.SetFollowAndFace(false,true), //仅面向用户
        _centerManager._UI.UI_FacePanel_2.ChangeTitel("Take_2_6"),  //切换标题
        _centerManager._ItemFacePanel.SwitchForm(2), //将面板形态切换至第三形态
        ReadAlondAndShow("Take_2_3","Take_2_3"),    //朗读并显示老师介绍_1
        WaitTime(3f),
        ReadAlondAndShow("Take_2_4","Take_2_4"),    //朗读并显示老师介绍_2
        WaitTime(3f),
        ReadAlondAndShow("Take_2_5","Take_2_5"),    //朗读并显示老师介绍_3
    };
    
    /// <summary>
    /// 朗读并显示
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReadAlondAndShow(string contentKey,string readalondKey)
    {
        IEnumerator[] Itemp = new IEnumerator[]
        {
            //显示TMP
            _centerManager._UI.UI_FacePanel_2.ChangeContent(contentKey), 
            //朗读文本
            _centerManager._Talk.PlayAudioCoroutine(readalondKey)
        };
        //Itemp并发启动
        yield return StartCoroutine(ConcurrentTake(Itemp));
        //隐藏TMP
        yield return StartCoroutine(_centerManager._UI.UI_Scence.HideTMP(0.3f));
    }
    
    
    
    
    
    
    
    
    
    /*/// <summary>
    /// 物品归位（子任务2入口）
    /// 第二阶段有三个状态
    ///     1.非手持状态
    ///     2.手持状态
    ///     3.物品进入展柜状态
    /// 三个状态之间形成闭环，只有正确才可跳出闭环
    /// </summary>
    /// <returns></returns>
    IEnumerator ForGrab()
    {
        Debug.Log("顺利进入第二阶段");
        for (int i=0; i<5;i++)
        {
            yield return StartCoroutine(State_unGrab());
            M_1_Item[0].GetComponent<XRGrabInteractable>().enabled = false;
            M_1_UI_PU.RemoveAt(0);
            M_1_Item.RemoveAt(0);
            M_1_ShowCase.RemoveAt(0);
            M_1_talk_1.RemoveAt(0);
            M_1_inGrag.RemoveAt(0);
        }
        Debug.Log("第二阶段 结束");
    }

    /// <summary>
    /// 非手持状态
    /// </summary>
    /// <returns></returns>
    IEnumerator State_unGrab()
    {
        Debug.Log("进入   非手持状态");
        //GameObject.Find("UI_Mission").GetComponent<UI_Mission>().UI_Mission_ChangeMission(M_1_UI_PU[0]);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(TalkManager.Instance.HeadInsertTalk(M_1_inGrag[0]));
        //等待进入手持状态
        while (true)
        {
            if (InteractionManager.Instance.HeldObject != null)
            {
                break;
            }

            yield return null;
        }

        yield return StartCoroutine(State_Grab());
    }

    /// <summary>
    /// 手持状态
    /// </summary>
    /// <returns></returns>
    IEnumerator State_Grab()
    {
        Debug.Log("进入   手持状态");
        if (InteractionManager.Instance.HeldObject != M_1_Item[0] &&
            InteractionManager.Instance.CheckHeldObjectChanged()) //拾取不正确
        {
            yield return StartCoroutine(TalkManager.Instance.HeadInsertTalk("grabError"));
            while (InteractionManager.Instance.HeldObject != null)  //等待放下物体
            {
                Debug.Log("拾取错误，请放下物体");
                yield return null;
            }

            yield return StartCoroutine(State_unGrab());
        }
        else //拾取正确
        {
            //GameObject.Find("UI_Mission").GetComponent<UI_Mission>().UI_Mission_ChangeMission("M_1_PTI_1");
            yield return StartCoroutine(TalkManager.Instance.HeadInsertTalk(M_1_talk_1[0]));
            while (true)
            {
                if (InteractionManager.Instance.HeldObject == null) //放下物品
                {
                    yield return new WaitForSeconds(1f);    //物理引擎更新可能会使对象瞬时重置，即下面的foreach在Item_ShowCase的OnTriggerEnter触发前遍历完（或者说遍历结束后Item_ShowCase中的nowItem才被赋值），因此暂停1秒让nowItem先被赋值
                    foreach (var SC in M_1_ShowCase.Select((value, i) => new { i, value }))
                    {
                        Debug.Log("遍历了");
                        Debug.Log(SC.value.GetComponent<Item_ShowCase>().nowItem );
                        if (SC.value.GetComponent<Item_ShowCase>().nowItem != null)
                        {
                            if (SC.i == 0)
                            {
                                //TODO:正确
                                Debug.Log("放的对");
                                yield return StartCoroutine(TalkManager.Instance.HeadInsertTalk("positionCorrect"));
                                yield break;
                            }
                            yield return StartCoroutine(State_putItemError(SC.i));
                            yield break;
                        }
                    }
                    yield return StartCoroutine(State_unGrab());
                    break;
                }
                yield return null;
            }
        }
    }

    /// <summary>
    /// 物品进入错误展柜状态
    /// </summary>
    /// <returns></returns>
    IEnumerator State_putItemError(int showCaseIndex)
    {
        Debug.Log("进入   物品进入错误展柜状态");
        yield return StartCoroutine(TalkManager.Instance.HeadInsertTalk("positionError"));
        yield return StartCoroutine(TalkManager.Instance.HeadInsertTalk(M_1_inGrag[0]));
        while (true)
        {
            Debug.Log($"fang错了");

            if (M_1_ShowCase[showCaseIndex].GetComponent<Item_ShowCase>().nowItem == null)
            {
                yield return StartCoroutine(State_Grab());
                break;
            }

            yield return null;
        }
    }*/
}