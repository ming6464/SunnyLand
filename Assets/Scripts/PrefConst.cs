using UnityEngine;

public class PrefConst : Singleton<PrefConst>
{
    private const string LASTLEVEL = "Last Level",SFXAUDIOVOL = "Sfx Audio Vol", MUSICAUDIOVOL = "Music Audio Vol";
    public int LastLevel
    {
        get => PlayerPrefs.GetInt(LASTLEVEL, 1);
        set
        {
            if (LastLevel < value)
            {
                PlayerPrefs.SetInt(LASTLEVEL, value);
            }
        }
    }

    public float SfxAudioVol
    {
        get => PlayerPrefs.GetFloat(SFXAUDIOVOL, 0.3f);
        set => PlayerPrefs.SetFloat(SFXAUDIOVOL, value);
    }

    public float MusicAudioVol
    {
        get => PlayerPrefs.GetFloat(MUSICAUDIOVOL, 0.1f);
        set => PlayerPrefs.SetFloat(MUSICAUDIOVOL, value);
    }
}