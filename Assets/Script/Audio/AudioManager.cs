using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioMixerGroup guitarMixer;

    int deviceID;

    [System.Serializable]
    public class AudioData
    {
        public float[] data;
        public int length;
        public int channels;
        public int frequency;
    }

    public bool StartPlaying(int deviceID, int length)
    {
        if (deviceID < 0) return false;

        if (length == 0) length = 10;
    
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

            audioSource.clip = Microphone.Start(Microphone.devices[deviceID], true, length, freq);
            audioSource.loop = true;

            while (!(Microphone.GetPosition(Microphone.devices[deviceID]) > 0)) { }
            audioSource.Play();

            return true;
        }

        return false;
    }

    public void StopPlaying(float length)
    {
        if (Microphone.devices.Length > deviceID)
        {
            Microphone.End(Microphone.devices[deviceID]);

            if (length > 0f)
            {
                //AudioClip audioRecording = AudioClip.Create("test", (int)(length * audioSource.clip.frequency), audioSource.clip.channels, audioSource.clip.frequency, false);
                float[] data = new float[(int)(length * audioSource.clip.frequency)];
                audioSource.clip.GetData(data, 0);
                //audioRecording.SetData(data, 0);
                AudioData audioData = new AudioData();
                audioData.data = data;
                audioData.length = (int)(length * audioSource.clip.frequency);
                audioData.channels = audioSource.clip.channels;
                audioData.frequency = audioSource.clip.frequency;

                string audioStr = JsonUtility.ToJson(audioData);
                System.IO.Directory.CreateDirectory(Application.dataPath + "/Saved/Audio");
                System.IO.File.WriteAllText(Application.dataPath + "/Saved/Audio/" + "test" + ".json", audioStr);
            }
        }
    }

    public int GetDeviceID()
    {
        return deviceID;
    }
}
