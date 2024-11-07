using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public abstract class Prop_Showcase_Base : MonoBehaviour,IInitialization
{
    public string DataSetSequence => GetType().ToString();
    public virtual IEnumerator Data_Set()
    {
        yield return null;
    }

    //进入事件
    public UnityEvent<GameObject,GameObject> EOnCollisionEnter;
    //离开事件
    public UnityEvent<GameObject,GameObject> EOnCollisionExit;
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Prop")
        {
            //Debug.Log("进入展柜");
            EOnCollisionEnter?.Invoke(other.gameObject, this.gameObject);
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Prop")
        {
            //Debug.Log("离开展柜");
            EOnCollisionExit?.Invoke(other.gameObject, this.gameObject);
        }
    }
}