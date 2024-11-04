using System.Collections;
using UnityEngine;

public class Talk_Player : Talk_Base
{
    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="_audioClip">音频文件</param>
    /// <returns></returns>
    public IEnumerator PlayTalk(AudioClip _audioClip)
    {
        AudioSource _audioSource = GetComponent<AudioSource>();
        yield return StartCoroutine(PlayAudioClip(_audioClip, _audioSource));
    }
}