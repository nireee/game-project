using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler Singlton;
    public AudioSource Background, FX;
    public AudioLibrary[] Tracks;
    private AudioLibrary backgroundTrack = null, fxTrack = null;

    public bool LoopBackground = true;
    public string DefaultBackgroundClip;

    [Range(0,1)] public float MasterVolume = 1;
    private float masterVolume;
    private bool mute = false;

    // Start is called before the first frame update
    void Start()
    {
        SetVolume(MasterVolume);
        if (Singlton == null)
        {
            DontDestroyOnLoad(gameObject);
            Singlton = this;
        }
        else Destroy(gameObject);

        StartBackground(DefaultBackgroundClip);
    }
    public static void Mute()
    {
        Singlton.Mute(!Singlton.mute);
    }
    public void Mute(bool state)
    {
        mute = state;
        if (mute) masterVolume = 0;
        else masterVolume = MasterVolume;
        if (backgroundTrack != null) Background.volume = masterVolume * backgroundTrack.volume;
        if (fxTrack != null) FX.volume = masterVolume * fxTrack.volume;
    }
    public void SetVolume(float volume)
    {
        MasterVolume = volume;
        masterVolume = MasterVolume;
    }

    private void startBackground(string clipName)
    {
        AudioLibrary track = getTrack(clipName);
        Background.clip = track.Track;
        Background.Play();
        Background.loop = LoopBackground;
        Background.volume = masterVolume * track.volume; 
    }
    public static void StartBackground(string clipName){ Singlton.startBackground(clipName); }

    private void playFX(string clipName)
    {
        AudioLibrary track = getTrack(clipName);
        FX.clip = track.Track;
        FX.Play();
        FX.loop = false;
        FX.volume = masterVolume * track.volume;
    }

    public static void PlayFX(string clipName){ Singlton.playFX(clipName); }

    private AudioLibrary getTrack(string name)
    {
        
        foreach(AudioLibrary track in Tracks)
        {
            if (name == track.Name) return track;
        }
        Debug.LogWarning("Track not found");
        return null;
    }
}

[System.Serializable]
public class AudioLibrary
{
    public string Name = "I need a name";
    public AudioClip Track;
    [Range(0, 1)] public float volume = 1;
}
