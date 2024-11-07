using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

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

        GameManager.Instance.ControllerUI.UIFace1.OpenNowUI(0f), //透明打开
        ReadAndShowUI("Take_2_3", "Take_2_3"),
        GameManager.Instance.ControllerUI.UIFace1.Fade(1f, 0f, 1f), //渐隐
        WaitTime(1f),
        ReadAndShowUI("Take_2_4", "Take_2_4"),
        GameManager.Instance.ControllerUI.UIFace1.Fade(1f, 0f, 1f), //渐隐
        WaitTime(1f),
        ReadAndShowUI("Take_2_5", "Take_2_5"),
        GameManager.Instance.ControllerUI.UIFace1.Fade(1f, 0f, 1f), //渐隐
        WaitTime(5f),
        GameManager.Instance.ControllerUI.UIFace1.Fade(1f, 0f, 1f), //渐隐
        GameManager.Instance.ControllerUI.UIFace1.ExitNowUI(), //关闭 Face_1 面板

        /* 教师导入部分完成 */
        GameManager.Instance.ControllerUI.UIScreen.Fade(1f, 0f, 1f),
        GameManager.Instance.ControllerUI.UIScreen.SetContent(uiData_Screen_Content_2),
        WaitTime(3f),
        GameManager.Instance.ControllerUI.UIScreen.Fade(0f, 1f, 1f),

        ReturnItem(),
    };

    /// <summary>
    /// 读取和渐显
    /// </summary>
    /// <param name="TalkKey">TalkKey</param>
    /// <param name="UIKey">UIKey</param>
    /// <returns></returns>
    public IEnumerator ReadAndShowUI(string TalkKey, [CanBeNull] string UIKey)
    {
        var uiData = GameManager.Instance.ControllerData.DataUI.GetData(UIKey);
        var talkData = GameManager.Instance.ControllerData.DataTalk.GetData(TalkKey);

        IEnumerator[] Itemp = new IEnumerator[]
        {
            //TMP 赋值
            GameManager.Instance.ControllerUI.UIFace1.SetContent(uiData),
            //渐显
            GameManager.Instance.ControllerUI.UIFace1.Fade(0f, 1f, 1f),
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

    public IEnumerator ReturnItem()
    {
        AudioClip talkData_Start = GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Start");
        AudioClip talkData_Tips = GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Tips");
        yield return StartCoroutine(GameManager.Instance.ControllerTalk.TalkPlayer.PlayTalk(talkData_Tips));
        yield return StartCoroutine(GameManager.Instance.ControllerTalk.TalkPlayer.PlayTalk(talkData_Start));

        yield return WaitTime(1f);

        GameObject nowHandGameObject = null;
        GameObject nowShowcaseGameObject = null;
        GameObject nowShowcase = null;

        GameManager.Instance.ControllerProp.PropPassport.RegisterGrab(args =>
        {
            nowHandGameObject = args.interactableObject.transform.gameObject;
            Debug.Log("对象已被选中: " + args.interactableObject.transform.gameObject);
        });
        GameManager.Instance.ControllerProp.PropPassport.WithDrowGrab(args =>
        {
            nowHandGameObject = null;
            Debug.Log("对象已被丢弃: " + args.interactableObject.transform.gameObject);
        });
        GameManager.Instance.ControllerProp.PropCharger.RegisterGrab(args =>
        {
            nowHandGameObject = args.interactableObject.transform.gameObject;
            Debug.Log("对象已被选中: " + args.interactableObject.transform.gameObject);
        });
        GameManager.Instance.ControllerProp.PropCharger.WithDrowGrab(args =>
        {
            nowHandGameObject = null;
            Debug.Log("对象已被丢弃: " + args.interactableObject.transform.gameObject);
        });
        GameManager.Instance.ControllerProp.PropSuitcase.RegisterGrab(args =>
        {
            nowHandGameObject = args.interactableObject.transform.gameObject;
            Debug.Log("对象已被选中: " + args.interactableObject.transform.gameObject);
        });
        GameManager.Instance.ControllerProp.PropSuitcase.WithDrowGrab(args =>
        {
            nowHandGameObject = null;
            Debug.Log("对象已被丢弃: " + args.interactableObject.transform.gameObject);
        });
        GameManager.Instance.ControllerProp.PropLuggage.RegisterGrab(args =>
        {
            nowHandGameObject = args.interactableObject.transform.gameObject;
            Debug.Log("对象已被选中: " + args.interactableObject.transform.gameObject);
        });
        GameManager.Instance.ControllerProp.PropLuggage.WithDrowGrab(args =>
        {
            nowHandGameObject = null;
            Debug.Log("对象已被丢弃: " + args.interactableObject.transform.gameObject);
        });
        GameManager.Instance.ControllerProp.PropTroller.RegisterGrab(args =>
        {
            nowHandGameObject = args.interactableObject.transform.gameObject;
            Debug.Log("对象已被选中: " + args.interactableObject.transform.gameObject);
        });
        GameManager.Instance.ControllerProp.PropTroller.WithDrowGrab(args =>
        {
            nowHandGameObject = null;
            Debug.Log("对象已被丢弃: " + args.interactableObject.transform.gameObject);
        });
        
        GameManager.Instance.ControllerProp.propShowcaseT11.EOnCollisionEnter.AddListener((args,sc) =>
        {
            nowShowcaseGameObject = args;
            nowShowcase = sc;
        });
        GameManager.Instance.ControllerProp.propShowcaseT12.EOnCollisionEnter.AddListener((args,sc) =>
        {
            nowShowcaseGameObject = args;
            nowShowcase = sc;
        });
        GameManager.Instance.ControllerProp.propShowcaseT13.EOnCollisionEnter.AddListener((args,sc) =>
        {
            nowShowcaseGameObject = args;
            nowShowcase = sc;
        });
        GameManager.Instance.ControllerProp.propShowcaseT14.EOnCollisionEnter.AddListener((args,sc) =>
        {
            nowShowcaseGameObject = args;
            nowShowcase = sc;
        });
        GameManager.Instance.ControllerProp.propShowcaseT15.EOnCollisionEnter.AddListener((args,sc) =>
        {
            nowShowcaseGameObject = args;
            nowShowcase = sc;
        });
        
        GameManager.Instance.ControllerProp.propShowcaseT11.EOnCollisionExit.AddListener((args,sc) =>
        {
            nowShowcaseGameObject = null;
            nowShowcase = null;
        });
        GameManager.Instance.ControllerProp.propShowcaseT12.EOnCollisionExit.AddListener((args,sc) =>
        {
            nowShowcaseGameObject = null;
            nowShowcase = null;
        });
        GameManager.Instance.ControllerProp.propShowcaseT13.EOnCollisionExit.AddListener((args,sc) =>
        {
            nowShowcaseGameObject = null;
            nowShowcase = null;
        });
        GameManager.Instance.ControllerProp.propShowcaseT14.EOnCollisionExit.AddListener((args,sc) =>
        {
            nowShowcaseGameObject = null;
            nowShowcase = null;
        });
        GameManager.Instance.ControllerProp.propShowcaseT15.EOnCollisionExit.AddListener((args,sc) =>
        {
            nowShowcaseGameObject = null;
            nowShowcase = null;
        });

        List<Type> Prop_Task_1 = new List<Type>()
        {
            typeof(Prop_Passport),
            typeof(Prop_Luggage),
            typeof(Prop_Suitcase),
            typeof(Prop_Troller),
            typeof(Prop_Charger),
        };

        List<string> PropShowCase_Task_1 = new List<string>()
        {
            typeof(Prop_Showcase_T1_1).ToString(),
            typeof(Prop_Showcase_T1_2).ToString(),
            typeof(Prop_Showcase_T1_3).ToString(),
            typeof(Prop_Showcase_T1_4).ToString(),
            typeof(Prop_Showcase_T1_5).ToString(),
        };

        List<AudioClip> AudioClips_T1_PickUp = new List<AudioClip>()
        {
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Passport_PickUp"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Luggage_PickUp"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Suitcase_PickUp"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Troller_PickUp"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Charger_PickUp"),
        };

        List<AudioClip> AudioClips_T1_Put = new List<AudioClip>()
        {
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Passport_Put"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Luggage_Put"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Suitcase_Put"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Troller_Put"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Charger_Put"),
        };
        List<AudioClip> AudioClips_T1_Error = new List<AudioClip>()
        {
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Passport_Error"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Luggage_Error"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Suitcase_Error"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Troller_Error"),
            GameManager.Instance.ControllerData.DataTalk.GetData("Take_2_6_Charger_Error"),
        };

        for (int i = 0; i < 5; i++)
        {
            nowShowcaseGameObject = null;
            nowShowcase = null;
            yield return StartCoroutine(P_H_State_1(i));
            Debug.Log("这波没了");
        }
        
        Debug.Log("任务1 完成");
        
        IEnumerator P_H_State_1(int index)
        {
           yield return StartCoroutine(GameManager.Instance.ControllerTalk.TalkPlayer.PlayTalk(AudioClips_T1_PickUp[index]));
           Debug.Log("请捡起来");
            while (nowHandGameObject == null)
            {
                yield return null;
            }

            yield return StartCoroutine(P_H_State_2(index));
        }

        IEnumerator P_H_State_2(int index)
        {
            // 使用 is 判断类型
            if (nowHandGameObject.GetComponent<Prop_Base>().GetType()==Prop_Task_1[index])
            {
                // 捡对了，请放下
                Debug.Log("捡对了，请放下");
                yield return StartCoroutine(GameManager.Instance.ControllerTalk.TalkPlayer.PlayTalk(AudioClips_T1_Put[index]));

                while (nowHandGameObject != null)
                {
                    yield return null;
                }

                // 检查是否放到了展示柜上
                if (nowShowcaseGameObject != null)
                {
                    yield return StartCoroutine(P_H_State_3(index));
                }
                else
                {
                    yield return StartCoroutine(P_H_State_1(index));
                }
            }
            else
            {
                // 捡错了，重新捡
                if (nowHandGameObject != null)
                {
                    Debug.Log("捡错了，重新捡");
                    yield return StartCoroutine(GameManager.Instance.ControllerTalk.TalkPlayer.PlayTalk(AudioClips_T1_Error[index]));
                }

                // 等待物品丢弃后重新开始
                while (nowHandGameObject != null)
                {
                    yield return null;
                }

                yield return StartCoroutine(P_H_State_1(index));
            }
        }


        IEnumerator P_H_State_3(int index)
        {
            if (nowShowcase.GetComponent<Prop_Showcase_Base>().GetType().ToString() == PropShowCase_Task_1[index])
            {
                Debug.Log("放对柜子了");
                yield return null;
            }
            else
            {
                Debug.Log("放错展柜了");
                while (nowHandGameObject.GetComponent<Prop_Base>().GetType() != Prop_Task_1[index])
                {
                    yield return null;
                }

                yield return StartCoroutine(P_H_State_2(index));
            }
        }
    }

    
}