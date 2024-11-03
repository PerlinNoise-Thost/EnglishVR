using System.Collections;
using UnityEngine;

public class Talk : MonoBehaviour
{
    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="keyCode">对应key值</param>
    /// <returns></returns>
    public IEnumerator PlayAudioCoroutine(string keyCode)
    {
        AudioSource audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audioSource.clip = CenterManager.Instance._Data.Data_Talk.GetAudioClip(keyCode); // 设置音频剪辑
        audioSource.Play();
        // 等待音频播放完成
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        audioSource.clip = null;
    }
}