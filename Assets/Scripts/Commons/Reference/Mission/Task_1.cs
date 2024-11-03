using System.Collections;
using System.Collections.Generic;
using Fungus;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class Task_1 : Task_Base
{
    /// <summary>
    /// 返回任务标识
    /// </summary>
    /// <returns></returns>
    public override int CurrentTaskIndex ()=> 2;
    public override List<IEnumerator> TaskSequence() => new List<IEnumerator>()
    {
        //显示并朗读TMP_1
       LearnHTCVive("Take_1_1"),
        //显示并朗读TMP_2
       LearnHTCVive("Take_1_2"),
        //显示并朗读TMP_3
       LearnHTCVive("Take_1_3"),
       //显示TMP_4
       _centerManager._UI.UI_Scence.ShowTMP(1,"Take_1_4"),
    };


    /// <summary>
    /// 朗读并显示
    /// </summary>
    /// <returns></returns>
    private IEnumerator LearnHTCVive(string keyCode)
    {
        IEnumerator[] Itemp = new IEnumerator[]
        {
            //显示TMP
            _centerManager._UI.UI_Scence.ShowTMP(1,keyCode), 
            //朗读文本
            _centerManager._Talk.PlayAudioCoroutine(keyCode)
        };
        //Itemp并发启动
        yield return StartCoroutine(ConcurrentTake(Itemp));
        //隐藏TMP
        yield return StartCoroutine(_centerManager._UI.UI_Scence.HideTMP(0.3f));
    }
}