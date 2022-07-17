using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCollision : MesFonctions
{
    GestionScriptsLance ScriptsLance;
    private void Start()
    {
        ScriptsLance = gestionLance(transform.parent);
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (ScriptsLance.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.Throwed)
        {
            ScriptsLance.ThrowLanceScript.StartstuckInAWall();
        }
        
    }
}
