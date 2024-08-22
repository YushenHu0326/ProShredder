using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExternalAudioPlayerUI : MonoBehaviour
{
    ExternalAudioPlayer player;

    public GameObject audioRegion;
    public GameObject audioSlider;

    // Start is called before the first frame update
    void Start()
    {
        ExternalAudioPlayer[] players = FindObjectsOfType(typeof(ExternalAudioPlayer)) as ExternalAudioPlayer[];
        if (players.Length > 0)
        {
            player = players[0];
        }
    }

    void Update()
    {
        if (audioRegion != null && audioSlider != null)
        {
            RectTransform regionTransform = audioRegion.GetComponent<RectTransform>();
            RectTransform sliderTransform = audioSlider.GetComponent<RectTransform>();

            if (regionTransform != null && sliderTransform != null)
            {
                float width = regionTransform.rect.width;
                if (player != null)
                {
                    Vector2 pos = sliderTransform.anchoredPosition;
                    pos.x = player.GetAudioPosition() * width;
                    sliderTransform.anchoredPosition = pos;
                }
            }
        }
    }

    public void PlayAudio()
    {
        if (player != null)
        {
            player.PlayAudio();
        }
    }

    public void PauseAudio()
    {
        if (player != null)
        {
            player.PauseAudio();
        }
    }

    public void StopAudio()
    {
        if (player != null)
        {
            player.StopAudio();
        }
    }

    public void ChangeVolume(float volume)
    {
        if (player != null)
        {
            player.ChangeVolume(volume);
        }
    }
}
