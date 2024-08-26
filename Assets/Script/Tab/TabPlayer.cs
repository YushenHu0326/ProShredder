using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabPlayer : MonoBehaviour
{
    public GameObject mainSection;
    public Tab tab;

    int sectionIndex;

    RectTransform rectTransform;
    RectTransform sectionTransform;

    public GameObject mainSectionTransform;
    public GameObject previousSectionTransform;
    public GameObject nextSectionTransform;

    ExternalAudioPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        sectionTransform = mainSection.GetComponent<RectTransform>();

        ExternalAudioPlayer[] players = FindObjectsOfType(typeof(ExternalAudioPlayer)) as ExternalAudioPlayer[];
        if (players.Length > 0)
        {
            player = players[0];
        }

        sectionIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && tab != null)
        {
            if (!player.IsPlaying()) return;

            float currentTime = Mathf.Max(player.currentTime - player.truncateTime, 0f);

            int currentSectionIndex = 0;
            float nextSectionStartTime = 0f;
            int totalSections = tab.GetSectionTotal();
            for (int i = 0; i < totalSections; i++)
            {
                currentSectionIndex = i;
                nextSectionStartTime += 8f * 60f / (float)tab.GetSectionBPM(i);
                if (nextSectionStartTime > currentTime) break;
            }

            for (int i = 0; i < currentSectionIndex; i++)
            {
                currentTime -= 8f * 60f / (float)tab.GetSectionBPM(i);
            }

            if (currentSectionIndex != sectionIndex)
            {
                sectionIndex = currentSectionIndex;
                tab.CycleSection(sectionIndex);
                tab.UpdateSectionDisplay(sectionIndex, mainSectionTransform.transform, previousSectionTransform.transform, nextSectionTransform.transform);
            }

            if (rectTransform != null && sectionTransform != null)
            {
                Vector2 pos = rectTransform.anchoredPosition;

                if (currentTime > 8f * 60f / (float)tab.GetSectionBPM(sectionIndex))
                {
                    pos.x = sectionTransform.rect.width;
                }
                else
                {
                    pos.x = currentTime / (8f * 60f / (float)tab.GetSectionBPM(sectionIndex)) * sectionTransform.rect.width;
                }

                rectTransform.anchoredPosition = pos;
            }
        }
    }
}
