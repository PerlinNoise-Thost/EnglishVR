using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Data_Talk : Data_Base<Data_Talk>
{
    //数据的结构
    private class _Data
    {
        public string ID;
        public string NPC;
        public string Content;
        public AudioClip audio;
    }
    
    //字典
    [ShowInInspector]
    [Searchable]
    //[ReadOnly]
    private Dictionary<string, _Data> dataSheet;
    
    //路径
    public override string path => "Assets/Resources/Form/Talk.xlsx";
    //长度
    public int length = 3;
    //本地读取or在线语音合成
    [Header("本地读取 or 在线语音合成")] public SynthesisMethod synthesisMethod;
    public enum SynthesisMethod
    {
        Local,
        Online
    }
    
    /// <summary>
    /// 读取表格
    /// </summary>
    /// <param name="content">字典</param>
    /// <typeparam name="T">类</typeparam>
    /// <returns></returns>
    public IEnumerator Load_Data_Talk()
    {
        dataSheet = new Dictionary<string, _Data>();
        yield return StartCoroutine(LoadCFG(length, dataSheet));

        switch (synthesisMethod)
        {
            case SynthesisMethod.Online: 
                yield return StartCoroutine(BaiDuSTT());
                break;
            case SynthesisMethod.Local:
                yield return StartCoroutine(LocalLoad(dataSheet));
                break;
                
        }
    }

    /// <summary>
    /// 本地读取音频文件
    /// </summary>
    /// <param name="datas">数据字典</param>
    /// <returns></returns>
    private IEnumerator LocalLoad(Dictionary<string,_Data> datas)
    {
        foreach (var data in datas)
        {
            //data.Value.audio = Resources.Load<AudioClip>(data.Value.Content);
            data.Value.audio = Resources.Load<AudioClip>("Test");
        }
        
        yield break;
    }

    /// <summary>
    /// 百度在线STT
    /// </summary>
    /// <returns></returns>
    private IEnumerator BaiDuSTT()
    {
        //TODO:百度在线语音合成(在其他脚本里)
        
        yield break;
    }
    
    /// <summary>
    /// 获取音频
    /// </summary>
    /// <param name="key">key</param>
    /// <returns></returns>
    public AudioClip GetData(string key) => dataSheet[key].audio;
}
