using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class SourceAudioDropdown : MonoBehaviour
{
    Dropdown dropdown;
    List<string> options;
    List<string> supportedFormat;

    // Start is called before the first frame update
    void Start()
    {
        GetSourceAudios();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GetSourceAudios();
    }

    void GetSourceAudios()
    {
        dropdown = GetComponent<Dropdown>();
        options = new List<string> { "No File Selected" };

        supportedFormat = new List<string> { "mp3", "wav", "ogg", "aif" };

        if (dropdown != null)
        {
            dropdown.ClearOptions();
            DirectoryInfo directory = new DirectoryInfo(Application.dataPath + "/Audio/");
            FileInfo[] infos = directory.GetFiles("*.*");
            foreach (FileInfo info in infos)
            {
                if (supportedFormat.Contains(info.Name.Substring(info.Name.Length - 3)) && !info.Name.Contains(" "))
                {
                    options.Add(info.Name);
                }
            }

            dropdown.AddOptions(options);
        }
    }

    public void LoadAudioFromFile()
    {
        if (dropdown != null)
        {
            if (dropdown.value > 0)
            {
                string path = Application.dataPath + "/Audio/" + options[dropdown.value];
                ExternalAudioPlayer[] audioPlayers = FindObjectsOfType(typeof(ExternalAudioPlayer)) as ExternalAudioPlayer[];
                if (audioPlayers.Length > 0)
                {
                    audioPlayers[0].audioFilePath = path;
                    audioPlayers[0].LoadAudio();
                }
            }
            else
            {
                ExternalAudioPlayer[] audioPlayers = FindObjectsOfType(typeof(ExternalAudioPlayer)) as ExternalAudioPlayer[];
                if (audioPlayers.Length > 0)
                {
                    audioPlayers[0].UnloadAudio();
                }
            }
        }
    }
}
