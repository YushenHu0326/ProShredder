using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public AudioDeviceDropdown audioDropdown;
    public VideoDeviceDropdown videoDropdown;

    public WebCamFeed webCamFeed;

    public GameObject buttons;

    bool audioIsPlaying, videoIsPlaying;

    void Start()
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            Color c = image.color;
            c.a = 0.5f;
            image.color = c;
        }
    }

    public void PlayStopAudio()
    {
        AudioManager[] audioManagers = FindObjectsOfType(typeof(AudioManager)) as AudioManager[];
        if (audioManagers.Length > 0 && audioDropdown != null)
        {
            if (audioIsPlaying)
            {
                audioManagers[0].StopPlaying();
                audioIsPlaying = false;
            }
            else
            {
                bool playing = audioManagers[0].StartPlaying(audioDropdown.GetDeviceID());
                audioIsPlaying = playing;
            }
        }
    }

    public void PlayStopVideo()
    {
        if (videoDropdown != null && webCamFeed != null)
        {
            if (videoIsPlaying)
            {
                bool stopped = webCamFeed.StopVideo(videoDropdown.GetDeviceName());
                videoIsPlaying = !stopped;
            }
            else
            {
                bool playing = webCamFeed.StartVideo(videoDropdown.GetDeviceName());
                videoIsPlaying = playing;
            }
        }
    }
}
