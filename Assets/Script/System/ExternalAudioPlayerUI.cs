using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExternalAudioPlayerUI : MonoBehaviour, IDragHandler, IDropHandler, IPointerClickHandler
{
    ExternalAudioPlayer player;

    public GameObject audioRegion;
    public GameObject audioSlider;

    private bool isDraggingSlider;

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
                if (player != null && !isDraggingSlider)
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
