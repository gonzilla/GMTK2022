using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MesFonctions
{
    public float OverrideTime;

    public void resetMe() 
    {
        if (OverrideTime!=0)
        {
            Invoke("reseet", OverrideTime);
        }
        else 
        {
            Invoke("reseet", Datas.LesDatas.TimeResetPlat);
        }
        
    }
    void reseet() 
    {
        GetComponent<PlatformEffector2D>().rotationalOffset = 0;
    }
}
