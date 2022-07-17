using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datas : MonoBehaviour
{
    public static Datas LesDatas;
    public Transform Player;
    public Camera MainCam;
    public Transform ThrowParent;
    public Transform CombatParent;
    public Transform Lance;
    public LayerMask Normal;
    public LayerMask Epee;
    public float TimeResetPlat;

    private void Awake()
    {
        if (LesDatas!=this)
        {
            LesDatas = this;
        }
    }
    
}
