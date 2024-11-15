using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    #region 单例模式

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    private void IAI()
    {
        // 如果实例尚未初始化，设置为当前实例
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            // 如果已经存在一个实例，并且不是当前实例，则销毁当前实例
            Destroy(gameObject);
        }
    }

    #endregion
    
    //相机
    public MainCamera MainCamera;
    //数据控制
    public Controller_Data ControllerData;
    //任务控制
    public Controller_Task ControllerTask;
    //UI控制
    public Controller_UI ControllerUI;
    //Talk控制
    public Controller_Talk ControllerTalk;
    //路线导航
    public Controller_NAV ControllerNav;
    //Prop控制
    public Controller_Prop ControllerProp;
    //NPC控制
    public Controller_NPC ControllerNpc;
    
    public void Awake()
    {
        IAI();
        DontDestroyOnLoad(this);
    }
}
