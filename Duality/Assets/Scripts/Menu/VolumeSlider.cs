using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    Slider _volumeSlider;
    float volume;

    void Start()
    {
        _volumeSlider = GetComponent<Slider>();
        PlayerPrefs.GetFloat("masterVolume", volume);
    }

    void Update()
    {
        volume = _volumeSlider.value;
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", volume);
    }
}
