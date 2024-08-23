using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioDeviceDropdown : MonoBehaviour, IPointerClickHandler
{
    Dropdown dropdown;
    List<string> options;

    // Start is called before the first frame update

    void Start()
    {
        GetAudioDevices();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GetAudioDevices();
    }

    void GetAudioDevices()
    {
        dropdown = GetComponent<Dropdown>();
        options = new List<string> { "No Audio Input" };

        if (dropdown != null)
        {
            dropdown.ClearOptions();

            if (Microphone.devices.Length > 0)
            {
                foreach (var device in Microphone.devices)
                {
                    options.Add(device);
                }

                dropdown.AddOptions(options);
            }
        }
    }

    public int GetDeviceID()
    {
        if (dropdown != null)
        {
            if (dropdown.value > 0 && dropdown.value <= Microphone.devices.Length)
            {
                return dropdown.value - 1;
            }
        }

        return -1;
    }
}
