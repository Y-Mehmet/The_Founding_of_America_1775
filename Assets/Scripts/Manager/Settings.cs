using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider musicSlider, soundSlider;

    private void OnEnable()
    {
        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);

        // Kaydýrýcýlarý mevcut ses seviyesine ayarla
        musicSlider.value = SoundManager.instance.musicVolume;
        soundSlider.value = SoundManager.instance.soundVolume;
    }

    private void OnDisable()
    {
        musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
        soundSlider.onValueChanged.RemoveListener(OnSoundSliderValueChanged);
    }

    void OnMusicSliderValueChanged(float value)
    {
        SoundManager.instance.SetMusicVolume(value);
    }

    void OnSoundSliderValueChanged(float value)
    {
        SoundManager.instance.SetSoundVolume(value);
    }
}
