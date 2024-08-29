using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioRecorder : MonoBehaviour, IDragHandler, IDropHandler
{
    ExternalAudioPlayer player;
    AudioManager audioManager;

    public GameObject audioRegion;
    public GameObject audioSlider;

    public GameObject regionSlider_1;
    public GameObject regionSlider_2;

    public GameObject[] recordUIs;

    bool isDraggingSlider1, isDraggingSlider2;
    float recordStartTime, recordEndTime;
    float recordLength;
    float currentTime;
    bool isRecording;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager[] managers = FindObjectsOfType(typeof(AudioManager)) as AudioManager[];
        if (managers.Length > 0) audioManager = managers[0];

        ExternalAudioPlayer[] players = FindObjectsOfType(typeof(ExternalAudioPlayer)) as ExternalAudioPlayer[];
        if (players.Length > 0) player = players[0];

        recordStartTime = 0f;
        recordEndTime = 1f;
        recordLength = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            float sliderPosX = 0f;

            RectTransform regionTransform = audioRegion.GetComponent<RectTransform>();
            RectTransform sliderTransform = audioSlider.GetComponent<RectTransform>();

            float width = regionTransform.rect.width;

            if (player != null)
            {
                if (player.AudioLoaded())
                {
                    foreach (GameObject ui in recordUIs)
                        ui.SetActive(false);

                    Vector2 pos = sliderTransform.anchoredPosition;
                    pos.x = player.GetAudioPosition() * width;
                    sliderTransform.anchoredPosition = pos;
                    sliderPosX = pos.x;
                }
                else
                {
                    foreach (GameObject ui in recordUIs)
                        ui.SetActive(true);

                    Vector2 pos = sliderTransform.anchoredPosition;
                    pos.x = currentTime / ((recordEndTime - recordStartTime) * recordLength) * width;
                    sliderTransform.anchoredPosition = pos;
                    sliderPosX = pos.x;
                }
            }

            RectTransform slider1Transform = regionSlider_1.GetComponent<RectTransform>();
            RectTransform slider2Transform = regionSlider_2.GetComponent<RectTransform>();

            Vector2 pos1 = slider1Transform.anchoredPosition;
            pos1.x = Mathf.Clamp(pos1.x, 0f, width);

            Vector2 pos2 = slider2Transform.anchoredPosition;
            pos2.x = Mathf.Clamp(pos2.x, 0f, width);

            if (player != null)
            {
                if (!player.AudioLoaded())
                {
                    pos1.x = 0f;
                    pos2.x = width;

                    if (isRecording)
                    {
                        currentTime += Time.deltaTime;
                        if (currentTime > recordEndTime * recordLength)
                        {
                            EndRecording();
                        }
                    }
                    else
                    {
                        currentTime = recordStartTime * recordLength;
                    }
                }
            }

            slider1Transform.anchoredPosition = pos1;
            slider2Transform.anchoredPosition = pos2;

            float startPosX = Mathf.Min(slider1Transform.anchoredPosition.x, slider2Transform.anchoredPosition.x);
            float EndPosX = Mathf.Max(slider1Transform.anchoredPosition.x, slider2Transform.anchoredPosition.x);

            if (player.IsPlaying())
            {
                if ((sliderPosX > startPosX && sliderPosX < EndPosX) && !isRecording)
                {
                    StartRecording();
                }
                else if (sliderPosX > EndPosX && isRecording)
                {
                    EndRecording();
                }
            }
        }
    }

    public void StartRecording()
    {
        isRecording = true;
        if (audioManager != null)
        {
            audioManager.StopPlaying(0f);
            audioManager.StartPlaying(audioManager.GetDeviceID(), (int)Mathf.Ceil((recordEndTime - recordStartTime) * recordLength));
        }
    }

    public void EndRecording()
    {
        isRecording = false;
        if (audioManager != null)
        {
            audioManager.StopPlaying(currentTime - recordStartTime * recordLength);
        }

        currentTime = recordStartTime * recordLength;
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (isRecording) return;
        if (player != null)
            if (!player.AudioLoaded())
                return;

        if (audioRegion != null && (regionSlider_1 != null && regionSlider_2 != null))
        {
            RectTransform regionTransform = audioRegion.GetComponent<RectTransform>();
            RectTransform slider1Transform = regionSlider_1.GetComponent<RectTransform>();
            RectTransform slider2Transform = regionSlider_2.GetComponent<RectTransform>();

            if (slider1Transform.rect.Contains(slider1Transform.InverseTransformPoint(Input.mousePosition)))
            {
                Vector2 mousePos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(regionTransform, pointerEventData.position, null, out mousePos);
                
                if (slider1Transform != null && player != null)
                {
                    isDraggingSlider1 = true;

                    Vector2 pos = slider1Transform.anchoredPosition;
                    pos.x = mousePos.x + regionTransform.rect.width / 2f;
                    slider1Transform.anchoredPosition = pos;
                }
            }
            else if (slider2Transform.rect.Contains(slider2Transform.InverseTransformPoint(Input.mousePosition)))
            {
                Vector2 mousePos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(regionTransform, pointerEventData.position, null, out mousePos);

                if (slider2Transform != null && player != null)
                {
                    isDraggingSlider2 = true;

                    Vector2 pos = slider2Transform.anchoredPosition;
                    pos.x = mousePos.x + regionTransform.rect.width / 2f;
                    slider2Transform.anchoredPosition = pos;
                }
            }
        }
    }

    public void OnDrop(PointerEventData pointerEventData)
    {
        if (isRecording) return;
        if (player != null)
            if (!player.AudioLoaded())
                return;

        if (isDraggingSlider1)
        {
            isDraggingSlider1 = false;
        }

        if (isDraggingSlider2)
        {
            isDraggingSlider2 = false;
        }
    }
}
