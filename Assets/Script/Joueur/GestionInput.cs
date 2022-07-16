using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestionInput : MesFonctions
{
    PlayerInput PI;
    GestionScript GS;
    void Start()
    {
        PI = GetComponent<PlayerInput>();
        GS = FindGestionScript(this.transform);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GS.MouvementGestion.SetState(MouvementGestion.MesEtats.Saute);
        }
        
    }

    public void DeplacementLateral(InputAction.CallbackContext context)
    {
        GS.MouvementGestion.SetDirection(context.ReadValue<float>());
    }

    public void ThroughPlatform(InputAction.CallbackContext context)
    {

    }
}
