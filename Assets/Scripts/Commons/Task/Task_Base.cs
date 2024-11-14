using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Task_Base : MonoBehaviour,ITask,IInitialization
{
    public string DataSetSequence => GetType().ToString();
    public virtual IEnumerator Data_Set()
    {
        yield return null;
    }
    
    /// <summary>
    /// 任务排序用字段
    /// </summary>
    public virtual string TaskSetSequence => this.GetType().ToString();
    
    /// <summary>
    /// 子任务列表
    /// </summary>
    /// <returns></returns>
    public abstract List<IEnumerator> TaskSequence { get; }
    
    /// <summary>
    /// 任务并发
    /// </summary>
    /// <param name="coroutinesToRun">需要并发的任务</param>
    /// <returns></returns>
    public IEnumerator ConcurrentTake(params IEnumerator[] coroutinesToRun)
    {
        List<Coroutine> coroutines = new List<Coroutine>();

        foreach (var coroutine in coroutinesToRun)
        {
            coroutines.Add(StartCoroutine(coroutine));
        }

        foreach (var coroutine in coroutines)
        {
            yield return coroutine; 
        }
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
}