using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExternalAudioPlayerUI : MonoBehaviour, IDragHandler, IDropHandler, IPointerClickHandler
{
    ExternalAudioPlayer player;
    public AudioMixer mixer;

    public GameObject audioRegion;
    public GameObject audioSlider;

    public Dropdown countIn;

    public Text tuneIndicator;
    private int tune;

    private bool isDraggingSlider;

    // Start is called before the first frame update
    void Start()
    {
        ExternalAudioPlayer[] players = FindObjectsOfType(typeof(ExternalAudioPlayer)) as ExternalAudioPlayer[];
        if (players.Length > 0)
        {
            player = players[0];
        }

        if (countIn != null)
        {
            List<string> options = new List<string> { "No Count In", "1/4", "2/4", "3/4", "4/4" };
            countIn.ClearOptions();
            countIn.AddOptions(options);
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
                if (player != null && !isDraggingSlider)
                {
                    Vector2 pos = sliderTransform.anchoredPosition;
                    pos.x = player.GetAudioPosition() * width;
                    sliderTransform.anchoredPosition = pos;
                }
            }
        }

        if (tuneIndicator != null)
        {
            if (tune == 0)
            {
                tuneIndicator.text = "Original Tune";
            }
            else
            {
                string text = Mathf.Abs(tune).ToString();
                text += " Steps ";
                if (tune > 0)
                {
                    text += "Up";
                }
                else
                {
                    text += "Down";
                }

                tuneIndicator.text = text;
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

    public void AutoTruncate(bool autoTruncate)
    {
        if (player != null)
        {
            player.autoTruncate = autoTruncate;
        }
    }

    public void SetBPM(string input)
    {
        int bpm;
        bool parsed = int.TryParse(input, out bpm);
        if (parsed && player != null)
        {
            player.bpm = bpm;
        }
    }

    public void SetCountIn()
    {
        if (player != null && countIn != null)
        {
            player.countIn = countIn.value;
        }
    }

    public void TuneDown()
    {
        tune = tune - 1;
        if (mixer != null)
        {
            mixer.SetFloat("ExternalAudioPitch", Mathf.Pow(1.059f, tune));
        }
    }

    public void TuneUp()
    {
        tune = tune + 1;
        if (mixer != null)
        {
            mixer.SetFloat("ExternalAudioPitch", Mathf.Pow(1.059f, tune));
        }
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (audioRegion != null && audioSlider != null)
        {
            RectTransform regionTransform = audioRegion.GetComponent<RectTransform>();
            if (regionTransform.rect.Contains(regionTransform.InverseTransformPoint(Input.mousePosition)))
            {
                Vector2 mousePos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(regionTransform, pointerEventData.position, null, out mousePos);
                RectTransform sliderTransform = audioSlider.GetComponent<RectTransform>();
                if (sliderTransform != null && player != null)
                {
                    isDraggingSlider = true;
                    PauseAudio();

                    Vector2 pos = sliderTransform.anchoredPosition;
                    pos.x = mousePos.x + regionTransform.rect.width / 2f;
                    sliderTransform.anchoredPosition = pos;
                }
            }
            else
            {
                if (isDraggingSlider)
                {
                    RectTransform sliderTransform = audioSlider.GetComponent<RectTransform>();

                    isDraggingSlider = false;

                    if (player != null)
                    {
                        player.SetAudioPosition(sliderTransform.anchoredPosition.x / regionTransform.rect.width);
                        PlayAudio();
                    }
                }
            }
        }
    }

    public void OnDrop(PointerEventData pointerEventData)
    {
        if (!isDraggingSlider) return;

        if (audioRegion != null && audioSlider != null)
        {
            RectTransform regionTransform = audioRegion.GetComponent<RectTransform>();
            RectTransform sliderTransform = audioSlider.GetComponent<RectTransform>();

            isDraggingSlider = false;

            if (player != null)
            {
                player.SetAudioPosition(sliderTransform.anchoredPosition.x / regionTransform.rect.width);
                PlayAudio();
            }
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (audioRegion != null && audioSlider != null)
        {
            RectTransform regionTransform = audioRegion.GetComponent<RectTransform>();
            if (regionTransform.rect.Contains(regionTransform.InverseTransformPoint(Input.mousePosition)))
            {
                Vector2 mousePos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(regionTransform, pointerEventData.position, null, out mousePos);
                RectTransform sliderTransform = audioSlider.GetComponent<RectTransform>();
                if (sliderTransform != null && player != null)
                {
                    Vector2 pos = sliderTransform.anchoredPosition;
                    pos.x = mousePos.x + regionTransform.rect.width / 2f;
                    sliderTransform.anchoredPosition = pos;

                    PauseAudio();
                    player.SetAudioPosition(sliderTransform.anchoredPosition.x / regionTransform.rect.width);
                    PlayAudio();
                }
            }
        }
    }
}
