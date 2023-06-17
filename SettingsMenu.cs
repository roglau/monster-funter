using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider aSlider, mSlider;
    //public AudioSource a, m;
    public AudioMixer audioMixer;

    void Start()
    {   
        //if (!PlayerPrefs.HasKey("musicV"))
        //{
        //    PlayerPrefs.SetFloat("musicV", 1);
            
        //    MLoad();
        //}
        //else if(PlayerPrefs.HasKey("musicV"))
        //{
        //    //Debug.Log("p");
        //    MLoad();
        //}
        //if (!PlayerPrefs.HasKey("audioV"))
        //{
        //    PlayerPrefs.SetFloat("audioV", 1);
        //    ALoad();
        //}
        //else if(PlayerPrefs.HasKey("audioV"))
        //{
        //    ALoad();
        //}
    }

    //public void ALoad()
    //{
    //    aSlider.value = PlayerPrefs.GetFloat("audioV");
    //}
    
    public void SetAudioVolume(float vol)
    {
        audioMixer.SetFloat("audioMixer", vol);
    }


    public void SetMusicVolume(float vol)
    {
        audioMixer.SetFloat("musicMixer", vol);
    }

    //public void MLoad()
    //{
    //    mSlider.value = PlayerPrefs.GetFloat("musicV");
    //}

    //public void ChangeAudio()
    //{
    //    a.volume = aSlider.value;
    //    ASave();
    //}
    
    //public void ChangeMusic()
    //{
    //    m.volume = mSlider.value;
    //    MSave();
    //}

    //public void ASave()
    //{
    //    PlayerPrefs.SetFloat("audioV", aSlider.value);
    //}

    //public void MSave()
    //{
    //    PlayerPrefs.SetFloat("musicV", mSlider.value);
    //}

    public void SetQuality(int idx)
    {
        QualitySettings.SetQualityLevel(idx);
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void SetFullscreen(bool b)
    {
        Screen.fullScreen = b;
    }
}
