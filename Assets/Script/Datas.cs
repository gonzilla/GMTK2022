using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datas : MonoBehaviour
{
    public static Datas LesDatas;
    private void Awake()
    {
        if (LesDatas!=this)
        {
            LesDatas = this;
        }
    }
    
}
