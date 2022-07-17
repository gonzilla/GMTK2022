using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieLife : MesFonctions
{
    public float Life;
    GestionsSciptsEnnemies MesScripts;
    void Start()
    {
        MesScripts = GetEnnemieGestion(this.transform);
    }

    // Update is called once per frame
    public void RetireVie(float Aretirer) 
    {
        Life -= Aretirer;
        if (Life<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
