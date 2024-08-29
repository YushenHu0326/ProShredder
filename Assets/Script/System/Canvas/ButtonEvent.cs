using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ButtonEvent : MonoBehaviour
{
    public GameObject buttons;
    public GameObject tabs;
    public Text buttonToggler;
    public Text tabToggler;

    public AudioDeviceDropdown audioDropdown;
    public VideoDeviceDropdown videoDropdown;

    public WebCamFeed webCamFeed;

    public AudioMixer guitarMixer;

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
                audioManagers[0].StopPlaying(0f);
                audioIsPlaying = false;
            }
            else
            {
                bool playing = audioManagers[0].StartPlaying(audioDropdown.GetDeviceID(), 0);
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
        if (guitarMixer != null)
        {
            guitarDistorted = !guitarDistorted;
            if (guitarDistorted)
                {
                guitarMixer.SetFloat("DistortionEnabled", 0f);
                if (guitarDistortionText != null) guitarDistortionText.text = "Distortion On";
            }
            else
            {
                guitarMixer.SetFloat("DistortionEnabled", -80f);
                if (guitarDistortionText != null) guitarDistortionText.text = "Distortion Off";
            }
        }
    }

    public void SetGuitarDistortionLevel(float level)
    {
        if (guitarMixer != null)
        {
            guitarMixer.SetFloat("DistortionLevel", 0.9f + level / 12f);
        }
    }

    public void ToggleUI()
    {
        if (buttons != null && buttonToggler != null)
        {
            buttons.SetActive(!buttons.activeSelf);
            if (buttons.activeSelf)
            {
                buttonToggler.text = "Disable UI";
            }
            else
            {
                buttonToggler.text = "Enable UI";
            }
        }
    }

    public void ToggleTab()
    {
        if (tabs != null && tabToggler != null)
        {
            tabs.SetActive(!tabs.activeSelf);
            if (tabs.activeSelf)
            {
                tabToggler.text = "Disable Tab";
            }
            else
            {
                tabToggler.text = "Enable Tab";
            }
        }
    }
}
