using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip auc_jump, auc_colect, auc_death, auc_finsh;
    private AudioSource m_aus;


    public override void Awake()
    {
        
    }

    public override void Start()
    {
        m_aus = GetComponent<AudioSource>();
        
    }

    public void PlayAudioEffect(int state,float vol)
    {
        switch (state)
        {
            case 1:
                m_aus.PlayOneShot(auc_jump,vol);
                break;
            case 2:
                m_aus.PlayOneShot(auc_colect,vol);
                break;
            case 3:
                m_aus.Stop();
                m_aus.PlayOneShot(auc_death,vol);
                break;
            default:
                m_aus.Stop();
                m_aus.PlayOneShot(auc_finsh,vol);
                break;
        }
    }
}