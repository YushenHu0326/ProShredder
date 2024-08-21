using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public void StartPlaying(int deviceID)
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (Microphone.devices.Length > deviceID)
        {
            int minFreq, maxFreq, freq;
            Microphone.GetDeviceCaps(Microphone.devices[deviceID], out minFreq, out maxFreq);
            freq = Mathf.Min(44100, maxFreq);

            audioSource.clip = Microphone.Start(Microphone.devices[deviceID], true, 10, freq);
            audioSource.loop = true;

            while (!(Microphone.GetPosition(Microphone.devices[deviceID]) > 0)) { }
            audioSource.Play();
        }
    }
}
