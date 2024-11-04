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
                    _instance = new GameObject("CenterManager").AddComponent<GameManager>();
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

    

    [Header("任务执行排序")] 
    public List<string> Task_Execute_Key = new List<string>()
    {
        "Task_1"
    };

    //数据控制
    [FormerlySerializedAs("dataControllerBase")] [FormerlySerializedAs("dataBaseController")] [FormerlySerializedAs("DataController")] public Controller_Data controllerData;
    //任务控制
    [FormerlySerializedAs("taskControllerBase")] [FormerlySerializedAs("taskBaseController")] [FormerlySerializedAs("taskController")] [FormerlySerializedAs("TalkController")] public Controller_Task controllerTask;
    
    public void Awake()
    {
        IAI();
        DontDestroyOnLoad(this);
    }
}
