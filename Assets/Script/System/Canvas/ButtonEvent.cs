using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public AudioDeviceDropdown audioDropdown;
    public VideoDeviceDropdown videoDropdown;

    public WebCamFeed webCamFeed;

    bool audioIsPlaying, videoIsPlaying;

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
