using System.Collections;
using UnityEngine;

public class NPC_Base : MonoBehaviour,IInitialization
{
    public virtual string DataSetSequence => GetType().ToString();
    public virtual IEnumerator Data_Set()
    {
        yield return null;
    }
}