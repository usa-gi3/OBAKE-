using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer mixer;

    const string VOLUME_KEY = "MasterVolume";

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolume();
    }

    public void SetVolume(float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        mixer.SetFloat("MasterVolume", dB);

        PlayerPrefs.SetFloat(VOLUME_KEY, value);
    }

    void LoadVolume()
    {
        float value = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);
        SetVolume(value);
    }
}
