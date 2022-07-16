using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datas : MonoBehaviour
{
    public static Datas LesDatas;
    public Transform Player;
    public Camera MainCam;
    private void Awake()
    {
        if (LesDatas!=this)
        {
            LesDatas = this;
        }
    }
    
}
