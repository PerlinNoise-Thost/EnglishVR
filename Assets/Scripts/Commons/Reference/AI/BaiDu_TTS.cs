using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using NAudio.Wave;

public class BaiDu_TTS : MonoBehaviour,IInitialization
{
    //api key
    public string api_key = "xxxxxx";
    //secret key
    public string secret_key = "xxxxxxxxx";
    //Token Key
    public string token = String.Empty;
    //用户唯一标识（计算设备的uv值,文档有建议，可以写个方法自动获取）
    public string cuid = "00-93-37-FF-0A-D2";
    //客户端类型选择
    public string ctp = "1";
    //语言（只有zn）
    public string lan = "zh";
    //语速 0-15,5为中
    public int spd = 5;
    //语调 0-15,5为中
    public int pit = 5;
    //音量 0-9，5为中
    public int vol = 5;
    //音色
    public int[] timbre = new int[]
    {
        0,
        1,
        3,
        4,
        5003,
        5118,
        106,
        103,
        110,
        111,
        5,
    };
    
    //实现初始化接口排序属性
    public string DataSetSequence => GetType().Name;
    
    //实现初始化接口
    public IEnumerator Data_Set()
    {
        yield return StartCoroutine(GetToken());
    }
    /// <summary>
    /// 语音合成(流式)
    /// </summary>
    /// <param name="tex"></param>
    /// <param name="per"></param>
    /// <param name="outOfAction"></param>
    /// <returns></returns>
    public IEnumerator TextToSpeech_Stream(string tex, int per)
    {
        // 容器并折叠句子
        string[] Folding_Entences = CutOutToTex(tex);
    
        // 检查输入是否有效
        if (Folding_Entences == null || Folding_Entences.Length == 0)
        {
            Debug.LogError("输入的文本无效。");
            yield break; // 结束协程
        }
    
        // 二进制音频文件容器
        List<byte> BinaryMusic = new List<byte>();
    
        for (int i = 0; i < Folding_Entences.Length; i++)
        {
            // 合成语句二次编码
            string twiceEnCode = TwiceUrlencodeAndURL(Folding_Entences[i]);
        
            // 临时音频
            byte[] tempData = null;
        
            // 合成音频文件
            yield return StartCoroutine(GetTTS(twiceEnCode, per, (par) => tempData = par));
        
            // 拼接至 List
            BinaryMusic.AddRange(tempData);
        }

        // 拿到二进制数据
        byte[] binarBytes = BinaryMusic.ToArray();
    
        Debug.Log("语音合成完毕，可以开始");
    
        // 确保 outOfAction 不为 null
        //outOfAction.Invoke(ConvertMP3ToAudioClip(binarBytes));
    }
    
    /// <summary>
    /// 语音合成(非流式)
    /// </summary>
    /// <param name="tex">文本</param>
    /// <param name="per">音库（朗读人声）</param>
    /// <returns></returns>
    public IEnumerator TextToSpeech(string tex, int per,Action<AudioClip> outOfAction)
    {
        // 容器并折叠句子
        string[] Folding_Entences = CutOutToTex(tex);
    
        // 检查输入是否有效
        if (Folding_Entences == null || Folding_Entences.Length == 0)
        {
            Debug.LogError("输入的文本无效。");
            yield break; // 结束协程
        }
    
        // 二进制音频文件容器
        List<byte> BinaryMusic = new List<byte>();
    
        for (int i = 0; i < Folding_Entences.Length; i++)
        {
            // 合成语句二次编码
            string twiceEnCode = TwiceUrlencodeAndURL(Folding_Entences[i]);
        
            // 临时音频
            byte[] tempData = null;
        
            // 合成音频文件
            yield return StartCoroutine(GetTTS(twiceEnCode, per, (par) => tempData = par));
        
            // 拼接至 List
            BinaryMusic.AddRange(tempData);
        }

        // 拿到二进制数据
        byte[] binarBytes = BinaryMusic.ToArray();
    
        Debug.Log("正在进行语音合成");
    
        // 确保 outOfAction 不为 null
        outOfAction.Invoke(ConvertMP3ToAudioClip(binarBytes));
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
    /// tex二次编码（文档建议）
    /// </summary>
    /// <param name="tex">tex</param>
    /// <returns>Post Body</returns>
    private string TwiceUrlencodeAndURL(string tex)
    {
        // 第一次URL编码
        string encodedTex = UnityWebRequest.EscapeURL(tex);

        // 第二次URL编码
        string doubleEncodedTex = UnityWebRequest.EscapeURL(encodedTex);

        //返回二次编码
        return doubleEncodedTex;
    }

    /// <summary>
    /// TTS
    /// </summary>
    /// <param name="tex">内容</param>
    /// <param name="tempAction">接收数据的回调</param>
    /// <returns></returns>
    private IEnumerator GetTTS(string tex,int per,Action<byte[]> tempAction)
    {
        string _url = $"https://tsn.baidu.com/text2audio?tex={tex}&tok={token}&cuid={cuid}&ctp={ctp}&lan={lan}&spd={spd}&pit={pit}&vol={vol}&per={per}";
        
        using (UnityWebRequest request = new UnityWebRequest(_url, "POST"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.result);
                Debug.LogError("请求错误: " + request.error);
            }
            else
            {
                string contentType = request.GetResponseHeader("Content-Type");
                if (contentType != null && contentType.StartsWith("audio"))
                {
                    //Debug.Log("音频合成成功！");
                    //Debug.Log(_url);
                    byte[] audioData = request.downloadHandler.data; // 获取音频数据
                    tempAction(audioData);
                }
                else if (contentType != null && contentType.Contains("application/json"))
                {
                    string jsonResponse = request.downloadHandler.text;
                    Debug.LogError("合成失败，返回错误信息：" + jsonResponse);
                }
                else
                {
                    Debug.LogError("未知的返回类型: " + contentType);
                }
            }
        }
    }

    /// <summary>
    /// 合成语句切断（短语音合成语句字符60个左右为佳，这里设置的是50）
    /// </summary>
    /// <param name="tex">合成语句</param>
    /// <returns>切断的数组</returns>
    private string[] CutOutToTex(string tex)
    {
        //容器
        List<string> texData = new List<string>();
        //原始数据
        string oldTex = tex;
        //tex折叠次数
        int numberOfTexLength = (int)Math.Ceiling((float)tex.Length / 50);
        //开始折叠
        for (int i = 0; i < numberOfTexLength; i++)
        {
            if (tex.Length <= 50)
            {
                texData.Add(tex);
            }
            else
            {
                string temp = tex.Substring(0, 50);
                texData.Add(temp);
                tex = tex.Substring(50);
            }
        }

        return texData.ToArray();
    }
    
    /// <summary>
    /// 二进制MP3转AudioClip；
    /// </summary>
    /// <param name="mp3Data">二进制MP3文件</param>
    /// <returns></returns>
    private AudioClip ConvertMP3ToAudioClip(byte[] mp3Data)
    {
        using (MemoryStream mp3Stream = new MemoryStream(mp3Data))
        using (Mp3FileReader mp3Reader = new Mp3FileReader(mp3Stream))
        {
            // 获取 WaveStream 中的 WaveFormat 信息
            WaveFormat waveFormat = mp3Reader.WaveFormat;

            // 创建一个字节数组来存储解码后的PCM数据
            byte[] byteBuffer = new byte[mp3Reader.Length];

            // 读取PCM数据到 byteBuffer 中
            int bytesRead = mp3Reader.Read(byteBuffer, 0, byteBuffer.Length);

            // 将 byteBuffer 转换为 floatBuffer (将字节数据转换为浮点数)
            float[] floatBuffer = new float[bytesRead / 2]; // 每个样本2字节 (16位音频)
            for (int i = 0; i < floatBuffer.Length; i++)
            {
                // 将两个字节转换为一个16位的有符号整数 (short)，再转换为 float
                short sample = BitConverter.ToInt16(byteBuffer, i * 2);
                floatBuffer[i] = sample / 32768f; // 转换为 [-1, 1] 的范围
            }

            // 创建 AudioClip
            AudioClip audioClip = AudioClip.Create("MyAudioClip", floatBuffer.Length, waveFormat.Channels, waveFormat.SampleRate, false);
            audioClip.SetData(floatBuffer, 0);

            return audioClip;
        }
    }
}