using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task_Base : MonoBehaviour,ITask
{
    /// <summary>
    /// 任务排序用字段
    /// </summary>
    public virtual string TaskSetSequence => this.GetType().ToString();
    /// <summary>
    /// 子任务列表
    /// </summary>
    /// <returns></returns>
    public abstract List<IEnumerator> TaskSequence();

}