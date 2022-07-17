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
           
            if (collision.transform.CompareTag("Ennemies"))
            {
                ScriptsLance.ThrowLanceScript.TouchAnEnnemie(collision.gameObject);
            }
            else
            {
                ScriptsLance.ThrowLanceScript.StartstuckInAWall();
            }
        }
        if (ScriptsLance.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.Attacking)
        {
            
            if (collision.transform.CompareTag("Ennemies"))
            {
                
                ScriptsLance.CoupDepeeScript.AddEnnemies(collision.gameObject);
            }
        }
        
    }
}
