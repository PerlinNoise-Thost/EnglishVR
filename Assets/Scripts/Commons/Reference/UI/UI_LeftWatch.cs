using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class UI_LeftWatch : UI_Base
{
    //显示当前任务
    public Button Button_ShowNowMission;
    //设置
    public Button Button_Setting;
    //打开背包
    public Button Button_OpenBag;
    
    public XRBaseController xrController; // 你的XRController对象

    // 设置角度的判断范围
    public Vector3 minRotation;  // 最小旋转角度
    public Vector3 maxRotation;  // 最大旋转角度

    /// <summary>
    /// 初始化函数
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator data_Set()
    {
        StartCoroutine(monitor());
        
        yield break;
    }
    bool IsRotationInRange(Vector3 rotation)
    {
        // 检查每个轴的角度是否在范围内
        return rotation.x >= minRotation.x && rotation.x <= maxRotation.x &&
               rotation.y >= minRotation.y && rotation.y <= maxRotation.y &&
               rotation.z >= minRotation.z && rotation.z <= maxRotation.z;
    }
    

    public IEnumerator monitor()
    {
        while (true)
        {
            yield return null;

            //Debug.Log(Binding.Instance.Key_1_State);
            //Debug.Log(Binding.Instance.Key_2_State);

            if (Binding.Instance.Key_1_State && Binding.Instance.Key_2_State)
            {
                Debug.Log("双键已按下");
                // 获取手柄的欧拉角
                Vector3 currentRotation = xrController.transform.localEulerAngles;
                //Debug.Log(currentRotation);
                if (IsRotationInRange(currentRotation))
                {
                    Debug.Log("手柄角度在指定范围内！");
                }
            }
        }
    }
    
}