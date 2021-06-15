using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler StaticAudioHandler;
    public AudioSource Background, FX;
    public AudioLibrary[] Tracks;


    public bool LoopBackground = true;
    public string DefaultBackgroundClip;

    [Range(0, 1)] public float MasterVolume = 1;

    private float masterVolume;
    private bool mute = false;

    private AudioLibrary backgroundTrack = null, fxTrack = null;


    // Start is called before the first frame update
    void Start()
    {
        SetVolume(MasterVolume);
        if (StaticAudioHandler == null)
        {
            DontDestroyOnLoad(gameObject);
            StaticAudioHandler = this;
        }
        else Destroy(gameObject);

        StartBackground(DefaultBackgroundClip);

    }

    public static void Mute()
    {
        StaticAudioHandler.Mute(!StaticAudioHandler.mute);
    }
    public void Mute(bool State)
    {
        mute = State;
        if (mute)
        {
            masterVolume = 0;
        }
        else masterVolume = MasterVolume;
        if(backgroundTrack != null)
        {
            Background.volume = masterVolume * backgroundTrack.volume;
        }
        if(fxTrack != null)
        {
            FX.volume = masterVolume * fxTrack.volume;
        }
    }

    public void SetVolume(float volume)
    {
        MasterVolume = volume;
        masterVolume = MasterVolume;
    }

    private void StartBackground(string clipname)
    {
        AudioLibrary track = getTrack(clipname);
        Background.clip = track.Track;
        Background.Play();
        Background.loop = LoopBackground;
        Background.volume = masterVolume * track.volume;
    }
    public static void startBackground(string clipname)
    {
        StaticAudioHandler.StartBackground(clipname);
    }

    private void PlayFX(string clipname)
    {
        AudioLibrary track = getTrack(clipname);
        FX.clip = track.Track;
        FX.Play();
        FX.loop = false;
        FX.volume = masterVolume * track.volume;

    }

    public static void playFX(string clipname)
    {
        StaticAudioHandler.PlayFX(clipname);
    }

    private AudioLibrary getTrack(string name)
    {
        AudioClip clip = null;

        foreach(AudioLibrary track in Tracks)
        {
            if(name == track.Name)
            {
                return track;
            }
            
        }
        Debug.Log("Tracks not found");
        return null;
    }

}

[System.Serializable]
public class AudioLibrary
{
    public string Name = "Name";

    public AudioClip Track;

    [Range(0, 1)] public float volume = 1;

}