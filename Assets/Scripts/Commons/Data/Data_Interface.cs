using System.Collections;

/// <summary>
/// 统一初始化接口(Awake)
/// </summary>
public interface IInitialization
{
    public string DataSetSequence { get; } //初始化排序用字段
    IEnumerator Data_Set();  //初始化方法，由Manager统一初始化
}