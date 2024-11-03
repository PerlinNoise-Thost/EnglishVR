using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Serialization;

public abstract class Task_Base : MonoBehaviour,ITask,IInitialization
{
    protected CenterManager _centerManager;
    
    //返回当前任务索引
    public abstract int CurrentTaskIndex(); 
    //每个任务的任务序列
    public abstract List<IEnumerator> TaskSequence(); 
    //初始化数据
    public virtual IEnumerator _data_Set()
    {
        yield break;
    }

    public virtual IEnumerator WaitTime(float timer)
    {
        yield return new WaitForSeconds(timer);
    }
    
    //实现初始化接口排序属性
    public virtual string DataSetSequence =>GetType().Name;
    
    public IEnumerator Data_Set()
    {
        _centerManager = CenterManager.Instance;
        yield return StartCoroutine(_data_Set());
    }
    
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
}