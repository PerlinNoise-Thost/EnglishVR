using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Fungus;
using OfficeOpenXml;
using UnityEngine;

public class Data_Talk : Data_Base,IInitialization
{
    public class Talk_Data
    {
        public string ID;
        public string NPC;
        public string Content;
        public AudioClip audio;
    }
    
    private Dictionary<string, Talk_Data> Talk_CFG = new Dictionary<string, Talk_Data>();
    public override IEnumerator _data_Set()
    {
        yield return StartCoroutine(LoadCFG("Assets/Talk.xlsx",3,Talk_CFG));
        yield return StartCoroutine(ReadByBaiDuTTS());
    }

    /// <summary>
    /// 语音合成
    /// </summary>
    private IEnumerator ReadByBaiDuTTS()
    {
        foreach (var _value in Talk_CFG)
        {
            string tempText = _value.Value.Content;
            string npc = _value.Value.NPC;
            _value.Value.audio = (AudioClip)Resources.Load("Assets/Resources/Audio/M_1_charger.mp3");
            //yield return StartCoroutine(baiDuTts.TextToSpeech(tempText,0,(temp)=>_value.Value.audio = temp));
        }
        
        Debug.Log("@@@ 语音合成结束");
        
        yield break;
    }
    
    /// <summary>
    /// 获取数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public override string GetData(string key)
    {
        return Talk_CFG[key].Content;
    }

    public AudioClip GetAudioClip(string key)
    {
        return Talk_CFG[key].audio;
    }
    
    /// <summary>
    /// 获取NPC
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetTalkNPC(string key)
    {
        return Talk_CFG[key].NPC;
    }
}