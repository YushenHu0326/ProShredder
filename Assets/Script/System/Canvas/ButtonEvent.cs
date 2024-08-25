using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ButtonEvent : MonoBehaviour
{
    public AudioDeviceDropdown audioDropdown;
    public VideoDeviceDropdown videoDropdown;

    public WebCamFeed webCamFeed;

    public GameObject buttons;

    bool audioIsPlaying, videoIsPlaying;

    public Text guitarDistortionText;
    bool guitarDistorted;

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

    public void SetGuitarDistortion()
    {
        AudioManager[] audioManagers = FindObjectsOfType(typeof(AudioManager)) as AudioManager[];
        if (audioManagers.Length > 0)
        {
            if (audioManagers[0].guitarMixer != null)
            {
                guitarDistorted = !guitarDistorted;
                if (guitarDistorted)
                {
                    audioManagers[0].guitarMixer.SetFloat("DistortionEnabled", 0f);
                    if (guitarDistortionText != null) guitarDistortionText.text = "Distortion On";
                }
                else
                {
                    audioManagers[0].guitarMixer.SetFloat("DistortionEnabled", -80f);
                    if (guitarDistortionText != null) guitarDistortionText.text = "Distortion Off";
                }
            }
        }
    }

    public void SetGuitarDistortionLevel(float level)
    {
        AudioManager[] audioManagers = FindObjectsOfType(typeof(AudioManager)) as AudioManager[];
        if (audioManagers.Length > 0)
        {
            if (audioManagers[0].guitarMixer != null)
            {
                audioManagers[0].guitarMixer.SetFloat("DistortionLevel", 0.9f + level / 12f);
            }
        }
    }
}
