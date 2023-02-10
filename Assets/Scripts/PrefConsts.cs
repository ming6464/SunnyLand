using UnityEngine;

public class PrefConsts : Singleton<PrefConsts>
{
    private const string BESTSCORE = "BESTSCORE",GEM = "GEM",CHERRY = "CHERRY",ENEMYKILL = "ENEMYKILL";

    public int BestScore
    {
        get => PlayerPrefs.GetInt(BESTSCORE, 0);
        set
        {
            if (BestScore < value) PlayerPrefs.SetInt(BESTSCORE,value);
        }
    }
    public int Gem
    {
        get => PlayerPrefs.GetInt(GEM, 0);
        set
        {
            if (Gem != value) PlayerPrefs.SetInt(GEM,value);
        }
    }
    public int Cherry
    {
        get => PlayerPrefs.GetInt(CHERRY, 0);
        set
        {
            if (Cherry != value) PlayerPrefs.SetInt(CHERRY,value);
        }
    }
    public int EnemyKill
    {
        get => PlayerPrefs.GetInt(ENEMYKILL, 0);
        set
        {
            if (EnemyKill != value) PlayerPrefs.SetInt(ENEMYKILL,value);
        }
    }
    
}