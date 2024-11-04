using System.Collections;
using UnityEngine;

public abstract class Talk_Base : MonoBehaviour
{
    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="audioClip">音频文件</param>
    /// <param name="audioSource">播放源</param>
    /// <returns></returns>
    protected virtual IEnumerator PlayAudioClip(AudioClip audioClip,AudioSource audioSource)
    {
        audioSource.clip = audioClip;
        audioSource.Play();

        while (audioSource.isPlaying)
        {
            yield return null;
        }

        audioSource.clip = null;
    }
}