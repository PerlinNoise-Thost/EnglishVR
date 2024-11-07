using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public abstract class Prop_Base : MonoBehaviour,IInitialization
{
    #region IInitialization接口的实现

    public virtual string DataSetSequence => GetType().ToString();
    public virtual IEnumerator Data_Set()
    {
        interactable = GetComponent<XRGrabInteractable>();
        yield break;
    }

    #endregion
    
    private XRGrabInteractable interactable;

    public virtual void RegisterGrab(UnityAction<SelectEnterEventArgs> args)
    {
        // 注册 OnSelectEntered 事件
        interactable.selectEntered.AddListener(args);
    }

    public virtual void DisRegisterGrab(UnityAction<SelectEnterEventArgs> args)
    {
        // 注销 OnSelectEntered 事件
        interactable.selectEntered.RemoveListener(args);
    }
    
    public virtual void WithDrowGrab(UnityAction<SelectExitEventArgs> args)
    {
        // 注册 selectExited 事件
        interactable.selectExited.AddListener(args);
    }

    public virtual void DisWithDrowGrab(UnityAction<SelectExitEventArgs> args)
    {
        // 注销 selectExited 事件
        interactable.selectExited.RemoveListener(args);
    }
}