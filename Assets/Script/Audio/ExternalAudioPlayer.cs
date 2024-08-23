using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Audio;

public class ExternalAudioPlayer : MonoBehaviour
{
    public float startTime;
    public float truncateTime;
    public bool autoTruncate;
    public string audioFilePath;

    private AudioSource audioSource;
    private bool isLoading;
    private bool isPaused;

    public float currentTime;


    public int bpm;
    public int countIn;

    public AudioSource countInAudio;

    public AudioMixerGroup mixer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = false;
        audioSource.outputAudioMixerGroup = mixer;
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
        if (audioSource.isPlaying) return;

        if (isPaused)
        {
            isPaused = false;
            audioSource.time = currentTime;
            audioSource.Play();
        }
        else if (audioSource.clip != null && !isLoading)
        {
            StartCoroutine(PlayAudioIEnum());
        }
    }

    IEnumerator PlayAudioIEnum()
    {
        for (int i = 0; i < countIn; i++)
        {
            if (countInAudio != null)
            {
                countInAudio.PlayOneShot(countInAudio.clip, 1f);
            }
            yield return new WaitForSeconds(60f / (float)bpm);
        }

        if (autoTruncate)
        {
            startTime = truncateTime;
        }

        audioSource.time = startTime;
        audioSource.Play();
    }

    public void PauseAudio()
    {
        audioSource.Pause();
        isPaused = true;
        currentTime = audioSource.time;
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

    public void SetAudioPosition(float pos)
    {
        if (audioSource.clip != null)
        {
            currentTime = pos * (audioSource.clip.length - startTime);
        }
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

        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);


        int startIndex = 0;

        for (int i = 0; i < samples.Length; i++)
        {
            if (samples[i] > 0.0001)
            {
                startIndex = i;
                break;
            }
        }

        truncateTime = audioSource.clip.length * ((float)startIndex / (float)samples.Length);
    }
}
