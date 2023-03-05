using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sfxSound, musicSound;
    public AudioSource musicSource, sfxSource;
    public override void Start()
    {
        musicSource.loop = true;
        sfxSource.volume = PrefConst.Ins.SfxAudioVol;
        musicSource.volume = PrefConst.Ins.MusicAudioVol;
    }
    
    public void PlayAudio(string name, bool isSFX)
    {
        Sound sound;
        if (isSFX)
        {
            if (!sfxSource) return;
            sound = Array.Find(sfxSound, s => string.Equals(s.name, name));
            if (sound != null)
            {
                sfxSource.PlayOneShot(sound.auc);
            }
            return;
        }
        if(!musicSource) return;
        sound = Array.Find(musicSound, s => string.Equals(name, s.name));
        if (sound != null)
        {
            musicSource.Stop();
            musicSource.clip = sound.auc;
            musicSource.Play();
        }
        
    }

    public void PauseOrResumeMusic(bool isPause)
    {
        if(isPause)
            musicSource.Pause();
        else musicSource.Play();
    }

    public void StopMusic()
    {
        if(musicSource) musicSource.Stop();
    }

    public void AudioVol(float vol,bool isSFX)
    {
        if (isSFX)
        {
            PrefConst.Ins.SfxAudioVol = vol;
            sfxSource.volume = vol;
            return;
        }
        PrefConst.Ins.MusicAudioVol = vol;
        musicSource.volume = vol;
    }

}