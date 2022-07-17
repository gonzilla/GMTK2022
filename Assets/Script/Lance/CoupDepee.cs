using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoupDepee : MesFonctions
{
    public enum Attaque 
    {
    Simple,
    Estoc

    }
    public Attaque LAttaque;


    GestionScriptsLance LesScriptsLances;
    void Start()
    {
        LesScriptsLances = gestionLance(this.transform);
    }
}
