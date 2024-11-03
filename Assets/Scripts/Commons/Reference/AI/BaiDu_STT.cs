using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using NAudio.Wave;

public class BaiDu_STT : MonoBehaviour,IInitialization
{
    //API key
    public string API;
    //Secret key
    public string Secret;
    //Token
    private string Token;
    //Microphone脚本
    public Device_Microphone DM;
    
    //实现初始化接口排序属性
    public string DataSetSequence =>GetType().Name;
    
    /// <summary>
    /// 初始化
    /// </summary>
    public IEnumerator Data_Set()
    {
        yield return StartCoroutine(GetToken());
    }

    /// <summary>
    /// 启动STT
    /// </summary>
    /// <param name="tempCallback">传参回调</param>
    /// <returns></returns>
    public IEnumerator StartSTT(Action<string> tempCallback)
    {
        byte[] BT = null;
        yield return StartCoroutine(DM.StartMPTra((tempBT) => BT = tempBT));

        Debug.Log("正在语音识别，开始发送");
        string STTResult = null;
        yield return StartCoroutine(GetSTT(BT, (tempString) => STTResult = tempString));

        tempCallback(STTResult);
    }

    /// <summary>
    /// 结束STT
    /// </summary>
    public void EndSTT()
    {
        DM.EndMPTra();
    }
    
    /// <summary>
    /// 获取结果
    /// </summary>
    /// <param name="PCM">pcm文件</param>
    /// <param name="per">回调传值</param>
    /// <returns></returns>
    private IEnumerator GetSTT(byte[] PCM,Action<string> per)
    {
        if (PCM.Length > 2 * 1024 * 1024) // 2MB
        {
            Debug.LogError("PCM数据过大，无法发送请求。");
            Debug.Log(PCM.Length);
            yield break; // 提前返回
        }
        
        UploadData postData = new UploadData()
        {
            rate = 16000,
            token = Token,
            speech = Convert.ToBase64String(PCM),
            len = PCM.Length
        };
        
        string _url = $"https://vop.baidu.com/pro_api?cuid={postData.cuid}&token={Token}&dev_pid={postData.dev_pid}";
        
        using (UnityWebRequest request = new UnityWebRequest(_url, "POST"))
        {
            // 设置请求头
            request.SetRequestHeader("Content-Type", "application/json");

            // 将上传的数据序列化为JSON格式
            string body = JsonUtility.ToJson(postData);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(body);
            request.uploadHandler = new UploadHandlerRaw(data);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.result);
                Debug.LogError("请求错误: " + request.error);
            }
            else
            {
                string downLoadData =request.downloadHandler.text;
                DownloadData _downloadData = JsonUtility.FromJson<DownloadData>(downLoadData);

                if (_downloadData.err_no == 0)
                {
                    //回调传值
                    per(_downloadData.result[0]);
                }
                else
                {
                    Debug.LogError(_downloadData.err_no + _downloadData.err_msg+_downloadData.sn);
                }
            }
        }
    }
    
    /// <summary>
    /// 上传数据类
    /// </summary>
    private class UploadData
    {
        //格式
        public string format = "pcm";
        //采样率(固定值 1.6khz)
        public int rate = 16000;
        //声道数(仅支持 1)
        public int channel = 1;
        //用户唯一标识符
        public string cuid = "00-93-37-FF-0A-D2";
        //Token
        public string token = string.Empty;
        //极速版输入模型
        public int dev_pid = 80001;
        //本地语音文件的的二进制语音数据 ，需要进行base64 编码。与len参数连一起使用
        public string speech = string.Empty;
        //本地语音文件的字节数
        public int len = -1;
    }

    /// <summary>
    /// 下载数据类
    /// </summary>
    private class DownloadData
    {
        //错误码
        public int err_no;
        //错误码描述
        public string err_msg;
        //语音数据唯一标识
        public string sn;
        //数据
        public string[] result;
    }
    
    /// <summary>
    /// 获取Token
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetToken()
    {
        //获取token的api地址
        string _token_url =
            string.Format(
                "https://aip.baidubce.com/oauth/2.0/token" +
                "?client_id={0}&client_secret={1}&grant_type=client_credentials", API, Secret);
        using (UnityWebRequest request = new UnityWebRequest(_token_url, "POST"))
        {
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.isDone)
            {
                string msg = request.downloadHandler.text;
                TokenInfo mTokenInfo = JsonUtility.FromJson<TokenInfo>(msg);
                //保存Token
                Token = mTokenInfo.access_token;
            }
        }
    }
    
    /// <summary>
    /// 返回的token
    /// </summary>
    [System.Serializable]
    public class TokenInfo
    {
        public string access_token = string.Empty;
    }
}