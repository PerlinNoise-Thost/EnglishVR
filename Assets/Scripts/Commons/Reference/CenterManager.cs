using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

public class CenterManager : MonoBehaviour
{
    #region 单例模式

    private static CenterManager _instance;

    public static CenterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CenterManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("CenterManager").AddComponent<CenterManager>();
                }
            }

            return _instance;
        }
    }

    private void IAI()
    {
        // 如果实例尚未初始化，设置为当前实例
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            // 如果已经存在一个实例，并且不是当前实例，则销毁当前实例
            Destroy(gameObject);
        }
    }

    #endregion

    //UI
    public UI _UI;

    //Data
    public Data _Data;

    //Talk
    public Talk _Talk;
    
    //item
    public Item_FacePanel _ItemFacePanel;
    
    //初始化顺序
    [FormerlySerializedAs("InitializationOrder")] [Header("初始化顺序(按照IInitialization的DataSetSequence属性排序)")]
    public List<string> initializationOrder = new List<string>()
    {
        "Data_Talk",
        "Data_UI",
        "BaiDu_STT",
        "BaiDu_TTS",
        "BaiDu_LLM",
        "UI_LeftWatch",
        "UI_Scence",
        "Task_1",
        "Task_2",
        "Item_FacePanel"
    };

    //Awake完成标志
    private bool Awake_Flag = false;

    //任务列表
    private List<IEnumerator> Task_List = new List<IEnumerator>();

    #region 初始化

    private void Awake()
    {
        IAI(); //保证自己是唯一的（单例）
        StartCoroutine(_Awake());
    }

    IEnumerator _Awake()
    {
        yield return StartCoroutine(AllSet(initializationOrder));
        yield return StartCoroutine(AllTask(Task_List));

        StartCoroutine(TaskPool(Task_List)); //启动任务池

        Debug.Log("初始化完成");
        Awake_Flag = true;
        
    }

    /// <summary>
    /// 初始化所有实现set接口的对象
    /// </summary>
    public IEnumerator AllSet(List<string> InitializationOrder)
    {
        List<IInitialization> IIn = FindInterfaces<IInitialization>();

        //排序
        var sortedIIn = IIn.OrderBy(i => InitializationOrder.IndexOf(i.DataSetSequence)).ToList();

        // 输出排序后的顺序
        Debug.Log("排序后的顺序: " + string.Join(", ", sortedIIn.Select(i => i.DataSetSequence)));

        foreach (var item in sortedIIn)
        {
            yield return StartCoroutine(item.Data_Set());
        }

        Debug.Log("数据加载完毕");

        yield break;
    }




    /// <summary>
    /// 查找所有实现ITask接口的对象并排序
    /// </summary>
    /// <returns></returns>
    public IEnumerator AllTask(List<IEnumerator> Task)
    {
        // 查找所有实现ITask接口的对象
        List<ITask> IIT = FindInterfaces<ITask>();
        //排序
        IIT.Sort((task1, task2) => task1.CurrentTaskIndex().CompareTo(task2.CurrentTaskIndex()));

        foreach (var iit in IIT)
        {
            Task.AddRange(iit.TaskSequence());
        }

        Debug.Log("所有任务加载完毕");
        yield break;
    }

    #endregion

    #region 方法

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

    #endregion
}