using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BaiDu_LLM : MonoBehaviour, IInitialization
{
    public string token;
    //这里填写百度千帆大模型里的应用api key
    public string api_key = "xxxxxx";
    //这里填写百度千帆大模型里的应用secret key
    public string secret_key = "xxxxxxxxx";
    //模型选择
    public ModelType model;

    // 历史对话
    private List<message> historyList = new List<message>();

    /// <summary>
    /// 初始化
    /// </summary>
    public IEnumerator Data_Set()
    {
        //初始化文心一言,获取token
        yield return StartCoroutine(GetToken());
    }

    //实现排序属性
    public string DataSetSequence => GetType().Name;

    //发送的数据
    private class RequestData
    {
        //发送的消息
        public List<message> messages = new List<message>();

        //流式输出
        public bool stream = true;

        //人设
        public string system = string.Empty;
    }

    private class message
    {
        //角色
        public string role = string.Empty;

        //对话内容
        public string content = string.Empty;

        public message(string _role, string _content)
        {
            if (_role != "")
            {
                role = _role;
            }

            content = _content;
        }
    }

    //接收的数据
    private class ResponseData
    {
        //本轮对话的id 
        public string id = string.Empty;

        public int created;

        //表示当前子句的序号,只有在流式接口模式下会返回该字段
        public int sentence_id;

        //表示当前子句是否是最后一句,只有在流式接口模式下会返回该字段
        public bool is_end;

        //表示当前子句是否是最后一句,只有在流式接口模式下会返回该字段
        public bool is_truncated;

        //返回的文本
        public string result = string.Empty;

        //表示输入是否存在安全
        public bool need_clear_history;

        //当need_clear_history为true时，此字段会告知第几轮对话有敏感信息，如果是当前问题，ban_round=-1
        public int ban_round;

        //token统计信息，token数 = 汉字数+单词数*1.3 
        public Usage usage = new Usage();
    }

    private class Usage
    {
        //问题tokens数
        public int prompt_tokens;

        //回答tokens数
        public int completion_tokens;

        //tokens总数
        public int total_tokens;
    }

    /// <summary>
    /// 模型名称
    /// </summary>
    public enum ModelType
    {
        Qianfan_Chinese_Llama_2_7B, //中文对话（省钱）
        ERNIE_Novel_8K, //适合小说场景对话的，支持英文(会补充设定)
        ERNIE_Lite_8K, //轻量级别小模型，支持英文（目前免费）
        ERNIE_4_0_Turbo_8K, //旗舰款，好用，中英文（贵）
    }

    /// <summary>
    /// 获取资源
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private string GetModelType(ModelType type)
    {
        if (type == ModelType.Qianfan_Chinese_Llama_2_7B)
        {
            return "qianfan_chinese_llama_2_7b";
        }

        if (type == ModelType.ERNIE_Novel_8K)
        {
            return "ernie-novel-8k";
        }

        if (type == ModelType.ERNIE_Lite_8K)
        {
            return "ernie-lite-8k";
        }

        if (type == ModelType.ERNIE_4_0_Turbo_8K)
        {
            return "ernie-4.0-turbo-8k";
        }

        return "";
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
                "?client_id={0}&client_secret={1}&grant_type=client_credentials", api_key, secret_key);
        using (UnityWebRequest request = new UnityWebRequest(_token_url, "POST"))
        {
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.isDone)
            {
                string msg = request.downloadHandler.text;
                TokenInfo mTokenInfo = JsonUtility.FromJson<TokenInfo>(msg);
                //保存Token
                token = mTokenInfo.access_token;
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

    /// <summary>
    /// 发送数据
    /// </summary> 
    /// <param name="_postWord"></param>
    /// <param name="_callback"></param>
    /// <returns></returns>
    public IEnumerator LLM_Request(string talk, Action<string> callBack)
    {
        string url = "https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/chat/";
        string postUrl = url + GetModelType(model) + "?access_token=" + token;
        historyList.Add(new message("user", talk));
        RequestData postData = new RequestData
        {
            messages = historyList
        };
        using (UnityWebRequest request = new UnityWebRequest(postUrl, "POST"))
        {
            string jsonData = JsonUtility.ToJson(postData);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(data);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.responseCode == 200)
            {
                string _msg = request.downloadHandler.text;
                ResponseData response = JsonUtility.FromJson<ResponseData>(_msg);
                //加入历史数据
                historyList.Add(new message("assistant", response.result));

                //回调
                callBack(response.result);
            }
        }
    }

}