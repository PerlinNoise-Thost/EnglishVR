using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;

/// <summary>
/// 任务控制接口
/// </summary>
public interface ITask
{
    public string TaskSetSequence { get; }  //任务排序用字段
    List<IEnumerator> TaskSequence { get; } //当前任务的任务序列
}

/// <summary>
/// 事件管理器接口
/// </summary>
public interface IEventInfo
{
    
}