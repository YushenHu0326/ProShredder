using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExternalAudioPlayer : MonoBehaviour
{
    public float startTime;
    public string audioFilePath;

    private AudioSource audioSource;
    private bool isLoading;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
        }
    }

    public void LoadAudio()
    {
        if (audioFilePath.Length > 0)
        {
            StartCoroutine(LoadAudioIEnum());
        }
    }

    public void PlayAudio()
    {
        if (audioSource.clip != null && !isLoading)
        {
            audioSource.time = startTime;
            audioSource.Play();
        }
    }

    public void PauseAudio()
    {
        audioSource.Pause();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public float GetAudioPosition()
    {
        if (audioSource.clip != null)
        {
            return audioSource.time / (audioSource.clip.length - startTime);
        }

        return 0f;
    }

    private IEnumerator LoadAudioIEnum()
    {
        isLoading = true;

        AudioType type = AudioType.WAV;
        if (audioFilePath.Substring(audioFilePath.Length - 3).Equals("mp3"))
        {
            type = AudioType.MPEG;
        }
        else if (audioFilePath.Substring(audioFilePath.Length - 3).Equals("ogg"))
        {
            type = AudioType.OGGVORBIS;
        }
        else if (audioFilePath.Substring(audioFilePath.Length - 3).Equals("aif"))
        {
            type = AudioType.AIFF;
        }

        UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip("file:///" + audioFilePath, type);
        yield return req.SendWebRequest();
        AudioClip clip = DownloadHandlerAudioClip.GetContent(req);

        isLoading = false;

        audioSource.clip = clip;
    }
}
