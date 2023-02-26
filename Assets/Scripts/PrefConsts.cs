using UnityEngine;

public class PrefConsts : Singleton<PrefConsts>
{
    private const string GEM = "GEM",CHERRY = "CHERRY",ENEMYKILL = "ENEMYKILL";
    
    public int gem
    {
        get => PlayerPrefs.GetInt(GEM, 0);
        set
        {
            if (gem != value) PlayerPrefs.SetInt(GEM,value);
        }
    }
    public int cherry
    {
        get => PlayerPrefs.GetInt(CHERRY, 0);
        set
        {
            if (cherry != value) PlayerPrefs.SetInt(CHERRY,value);
        }
    }
    public int enemyKill
    {
        get => PlayerPrefs.GetInt(ENEMYKILL, 0);
        set
        {
            if (enemyKill != value) PlayerPrefs.SetInt(ENEMYKILL,value);
        }
    }
    
}