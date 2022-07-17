using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionEtatLance : MesFonctions
{
   
    public enum EtatLance 
    {
    Blocking,
    Attacking,
    Idle,
    PreparationThrow,
    Throwed,
    BackingThrow,
    InWall

    }
    public EtatLance CurrentState;

    GestionScriptsLance LesScriptsLances;

    public void SetState(EtatLance newState) 
    {
        CurrentState = newState;
    }

    

    void Start()
    {
        LesScriptsLances = gestionLance(this.transform);
    }
}
