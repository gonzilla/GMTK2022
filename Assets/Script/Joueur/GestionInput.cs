using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestionInput : MesFonctions
{
    PlayerInput PI;
    GestionScript GS;
    bool throwForBool;
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
        if (context.started)
        {
            GS.MouvementGestion.PassThroughPlatform();
        }
    }

    public void ThrowSword(InputAction.CallbackContext context) 
    {

        if (context.started)
        {

            GS.ScriptsLance.ThrowLanceScript.PrepareThrow();
        }
        if (context.canceled)
        {
            GS.ScriptsLance.ThrowLanceScript.Throw();
        }
        
        
    
    }
    public void attaque(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            print("attaqeu");
            GS.ScriptsLance.LesAttaques.PrepareAttack(true);
        }
        if (context.canceled)
        {
            GS.ScriptsLance.LesAttaques.PrepareAttack(false);
        }
    }

    public void Defense(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            
            GS.GestionDushield.shield.StartDefense();
        }
        if (context.canceled)
        {
            GS.GestionDushield.shield.EndBlock();
        }
    }
}
