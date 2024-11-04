using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;

/// <summary>
/// 统一初始化接口（Awake）
/// </summary>
public interface IInitialization
{
    public string DataSetSequence { get; } //初始化排序用字段
    IEnumerator Data_Set();  //初始化方法，由Manager统一初始化
}

/// <summary>
/// 任务控制接口
/// </summary>
public interface ITask
{
    int CurrentTaskIndex();  //返回当前任务的索引
    List<IEnumerator> TaskSequence(); //当前任务的任务序列
}

/// <summary>
/// 事件管理器接口
/// </summary>
public interface IEventInfo
{
    
}