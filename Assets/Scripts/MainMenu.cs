using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public UnityEngine.UI.Toggle toggle;
    public UnityEngine.UI.Slider slider;
    public GameObject SettingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("volume",1);
        bool mute = PlayerPrefs.GetString("mute", "no") == "no" ? false: true;
        VolumeSlider(volume);
        slider.value = volume;
        MuteToggle(mute);
        toggle.isOn = mute;

    }
    public void StartGameButton()
    {
        SceneManager.LoadScene(1);
    }
    bool side = false;



    public void SettingButton()
    {
        SettingsPanel.SetActive(!SettingsPanel.activeSelf);

    }

    public void VolumeSlider(float volume)
    {
        AudioHandler.StaticAudioHandler.SetVolume(volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void MuteToggle(bool value)
    {
        AudioHandler.StaticAudioHandler.Mute(value);
        if (value)
        {
            PlayerPrefs.GetString("mute", "yes");
        }
        else
        {
            PlayerPrefs.GetString("mute", "no");
        }
    }
}
