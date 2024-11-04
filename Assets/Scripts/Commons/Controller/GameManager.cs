using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<String> Data_Set_Key = new List<string>()
    {
        "Data_Talk",
        "Data_UI"
    };
    
    public void Awake()
    {
        IAI();
    }
}
