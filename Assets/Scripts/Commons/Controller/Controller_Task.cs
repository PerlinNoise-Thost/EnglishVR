using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Task : Controller_Base,IInitialization
{
    public string DataSetSequence => "TaskPool";
    public IEnumerator Data_Set()
    {
        //实现IInitialization接口的实例列表
        List<ITask> initializations = null;
        //查找实例并赋值
        yield return StartCoroutine(base.FindInterfaces<ITask>((tempData) => initializations = tempData));
        yield return StartCoroutine(base.SortByReference<ITask>(initializations, "TaskSetSequence",Task_Execute_Key));
        List<IEnumerator> tasks = new List<IEnumerator>();
        foreach (var task in initializations)
        {
            if (task.TaskSequence != null)
            {
                tasks.AddRange(task.TaskSequence);
            }
            else
            {
                Debug.LogWarning($"任务 {task} 的 TaskSequence 为 null");
            }
        }

        yield return StartCoroutine(TaskPool(tasks));
    }

    public Task_1 Task1;
    
    [Header("任务执行排序")] 
    public List<string> Task_Execute_Key = new List<string>()
    {
        "Task_1"
    };
    
    /// <summary>
    /// 任务池
    /// 同步任务在IEnumerator中使用yield return StartCoroutine启动
    /// 异步任务在IEnumerator使用StartCoroutine启动
    /// </summary>
    /// <param name="Tasks">任务列表</param>
    /// <returns></returns>
    private IEnumerator TaskPool(List<IEnumerator> Tasks)
    {
        foreach (var task in Tasks)
        {
            yield return StartCoroutine(task);
        }
        
        Debug.Log("所有任务已经完成");
    }
}