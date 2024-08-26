using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioMixerGroup guitarMixer;

    int deviceID;

    public bool StartPlaying(int deviceID)
    {
        if (deviceID < 0) return false;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.outputAudioMixerGroup = guitarMixer;

        if (Microphone.devices.Length > deviceID)
        {
            this.deviceID = deviceID;
            
            int minFreq, maxFreq, freq;
            Microphone.GetDeviceCaps(Microphone.devices[deviceID], out minFreq, out maxFreq);
            freq = Mathf.Min(44100, maxFreq);

            audioSource.clip = Microphone.Start(Microphone.devices[deviceID], true, 1, freq);
            audioSource.loop = true;

            while (!(Microphone.GetPosition(Microphone.devices[deviceID]) > 0)) { }
            audioSource.Play();

            return true;
        }

        return false;
    }

    public void StopPlaying()
    {
        if (Microphone.devices.Length > deviceID)
        {
            Microphone.End(Microphone.devices[deviceID]);
        }
    }
}
