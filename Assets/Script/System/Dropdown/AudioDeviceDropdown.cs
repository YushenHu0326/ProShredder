using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioDeviceDropdown : MonoBehaviour
{
    Dropdown dropdown;
    List<string> options;

    // Start is called before the first frame update
    void Start()
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
