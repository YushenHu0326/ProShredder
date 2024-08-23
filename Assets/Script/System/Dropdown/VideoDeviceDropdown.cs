using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VideoDeviceDropdown : MonoBehaviour, IPointerClickHandler
{
    Dropdown dropdown;
    List<string> options;

    // Start is called before the first frame update
    void Start()
    {
        GetVideoDevices();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GetVideoDevices();
    }

    void GetVideoDevices()
    {
        dropdown = GetComponent<Dropdown>();
        options = new List<string> { "No Video Input" };

        if (dropdown != null)
        {
            dropdown.ClearOptions();

            WebCamDevice[] devices = WebCamTexture.devices;
            if (devices.Length > 0)
            {
                foreach (var device in devices)
                {
                    options.Add(device.name);
                }

                dropdown.AddOptions(options);
            }
        }
    }

    public string GetDeviceName()
    {
        if (dropdown != null)
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            if (dropdown.value > 0 && dropdown.value <= devices.Length)
            {
                return devices[dropdown.value - 1].name;
            }
        }

        return "";
    }
}
