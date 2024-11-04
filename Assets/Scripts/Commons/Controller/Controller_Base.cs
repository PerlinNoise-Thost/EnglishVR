using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller_Base : MonoBehaviour
{
    /// <summary>
    /// 接口排序(参照List)
    /// </summary>
    /// <param name="IIn">实现同一接口的对象的列表</param>
    /// <param name="PROPName">排序需依据的属性的标识符</param>
    /// <param name="InitializationOrder">参照的顺序列表</param>
    /// <typeparam name="T">接口类型</typeparam>
    /// <returns></returns>
    public IEnumerator SortByReference<T>(List<T> IIn, string PROPName, List<string> InitializationOrder) 
    {
        // 获取排序后的列表
        var sortedIIn = IIn.OrderBy(i => 
        {
            var dataSetSequence = typeof(T).GetProperty(PROPName)?.GetValue(i);
            return InitializationOrder.IndexOf(dataSetSequence?.ToString());
        }).ToList();

        // 清空原始列表并添加排序后的结果
        IIn.Clear();
        IIn.AddRange(sortedIIn);

        Debug.Log("排序后的顺序: " + string.Join(", ", IIn.Select(i => 
        {
            var dataSetSequence = typeof(T).GetProperty(PROPName)?.GetValue(i);
            return dataSetSequence?.ToString();
        })));
    
        yield break;
    }
    
    /// <summary>
    /// 查找场景中实现指定接口的所有对象，并返回它们的对象列表
    /// </summary>
    /// <typeparam name="T">接口类型</typeparam>
    /// <returns>实现指定接口的对象的列表</returns>
    public List<T> FindInterfaces<T>() where T : class
    {
        // 查找场景中所有的MonoBehaviour对象
        MonoBehaviour[] allMonoBehaviours = FindObjectsOfType<MonoBehaviour>();

        // 创建一个List来保存实现指定接口的对象
        List<T> interfaces = new List<T>();

        // 遍历所有MonoBehaviour，检查是否实现了指定接口
        foreach (var monoBehaviour in allMonoBehaviours)
        {
            T myInterface = monoBehaviour as T;
            if (myInterface != null)
            {
                // 添加该对象到列表
                interfaces.Add(myInterface);
            }
        }

        return interfaces;
    }
}