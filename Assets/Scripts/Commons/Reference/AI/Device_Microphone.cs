using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device_Microphone : MonoBehaviour
{
    //麦克风名字
    public string microphoneName = null;
    //频率
    public int _frequency = 8000;

    /// <summary>
    /// 录制开始(用EndMPTra结束录制)
    /// </summary>
    /// <param name="tempCallback"></param>
    /// <returns></returns>
    public IEnumerator StartMPTra(Action<byte[]> tempCallback)
    {
        if (Microphone.IsRecording(microphoneName))
        {
            Debug.LogError("当前麦克风正在录制中");
            yield break;
        }
        
        //开始录音
        AudioClip tempAC = null;
        yield return StartCoroutine(StartTra((ac) => tempAC = ac));

        //结束录音
        byte[] tempBT = null;
        yield return StartCoroutine(ACToPCM(tempAC, (bt) => tempBT = bt));

        //回调传参
        tempCallback(tempBT);
    }

    /// <summary>
    /// 结束录制
    /// </summary>
    public void EndMPTra()
    {
        Microphone.End(microphoneName);
        Debug.Log("录制已结束");
    }
    
    /// <summary>
    /// 开始并记录数据
    /// </summary>
    /// <param name="tempAction">传参回调</param>
    /// <returns></returns>
    private IEnumerator StartTra(Action<AudioClip> tempAction)
    {
        AudioClip ta = Microphone.Start(microphoneName, false, 10, _frequency);

        Debug.Log("录制开始");

        // 等待10秒录音结束，或者手动结束录制
        float recordingDuration = 0f;
        while (Microphone.IsRecording(microphoneName))
        {
            recordingDuration += Time.deltaTime;
            if (recordingDuration >= 10f)
            {
                break;
            }
            yield return null;
        }

        // 手动停止录制
        Microphone.End(microphoneName);

        // 回调 AudioClip
        tempAction(ta);
    }


    /// <summary>
    /// AudioClip转换PCM(byte[])格式
    /// </summary>
    /// <param name="tempAC">AudioClip</param>
    /// <param name="tempCallback">传参回调</param>
    /// <returns></returns>
    private IEnumerator ACToPCM(AudioClip tempAC, Action<byte[]> tempCallback)
    {
        if (tempAC.channels != 1)
        {
            Debug.LogError("音频不是单声道。");
            yield break;
        }
        
        Debug.Log("录音采样率: " + tempAC.frequency);
        Debug.Log("录音声道数: " + tempAC.channels);
        
        float[] samples = new float[tempAC.samples];
        tempAC.GetData(samples, 0);
        short[] intData = new short[samples.Length];
        byte[] bytesData = new byte[samples.Length * 2];
        int rescaleFactor = 32767;
 
        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * rescaleFactor);
            byte[] byteArr = new byte[2];
            byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, i * 2);
        }

        // 回调传递 PCM 数据
        tempCallback(bytesData);

        yield break;
    }

}