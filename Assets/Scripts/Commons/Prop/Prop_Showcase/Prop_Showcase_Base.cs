using System;
using System.Collections;
using UnityEngine;


public abstract class Prop_Showcase_Base : MonoBehaviour,IInitialization
{
    public string DataSetSequence => GetType().ToString();
    public virtual IEnumerator Data_Set()
    {
        yield return null;
    }

    public void OnCollisionEnter(Collision other)
    {
        
    }
}