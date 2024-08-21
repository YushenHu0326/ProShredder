using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void StopAudio()
    {
        audioSource.Stop();
    }

    private IEnumerator LoadAudioIEnum()
    {
        isLoading = true;

        WWW url = new WWW(audioFilePath);
        yield return url;

        isLoading = false;

        audioSource.clip = url.GetAudioClip(false, true);
    }
}
