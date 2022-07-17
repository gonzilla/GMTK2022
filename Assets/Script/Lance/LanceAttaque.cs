using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceAttaque : MesFonctions
{
    public float TimeMaxAttaque;
    public float TimeMaxForEstoc;
    GestionScriptsLance GDS;
    GestionScript GestionScriptPlayer;
    [SerializeField] Animator mAnimator;
    
    public float animationSpeedAttSimp;
    public float animationSpeedEstoc;
    bool maintient;
    float TimeMaintien;
    float timeT;

    // Start is called before the first frame update
    void Start()
    {
        GDS = gestionLance(this.transform);
        GestionScriptPlayer = FindGestionScript(Datas.LesDatas.Player);
        mAnimator = GetComponent<Animator>();
        mAnimator.enabled = false;
        CalibrageAnimation();
    }

    void Update()
    {
        if (Time.time > timeT + TimeMaxAttaque && maintient)
        {
            maintient = false;
            DetermineAttack();
        }
    }

    public void PrepareAttack(bool maint) 
    {
        if (!Datas.LesDatas.Defending)
        {
            if (GDS.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.Idle && maint)
            {
                maintient = maint;
                timeT = Time.time;
                if (!mAnimator.isActiveAndEnabled)
                {
                    AbleAnimator();
                }

            }
            if (!maint)
            {
                maintient = maint;
                DetermineAttack();

            }
        }
       
        
        
       
    
    }
    void DetermineAttack() 
    {
        
        if (Time.time<timeT+TimeMaxForEstoc && timeT!=0)
        {
            AttaqueSimple();
        }
        else if (timeT != 0)
        
        {
            Estoc();
        }
        timeT = 0;
    }
    

    void AttaqueSimple() 
    {
        GDS.GestionEtatLance.CurrentState = GestionEtatLance.EtatLance.Attacking;
        getSens();
        mAnimator.SetTrigger("AttaqueSimple");
        GDS.CoupDepeeScript.setState(CoupDepee.Attaque.Simple);
        


    }
     void Estoc() 
    {
        GDS.GestionEtatLance.CurrentState = GestionEtatLance.EtatLance.Attacking;
        getSens();
        mAnimator.SetTrigger("Estoc");
        GDS.CoupDepeeScript.setState(CoupDepee.Attaque.Estoc);

    }
    void CalibrageAnimation() 
    {

        mAnimator.SetFloat("AttaqueSimpleSpeed", animationSpeedAttSimp);
        mAnimator.SetFloat("EstocSpeed", animationSpeedEstoc);
    }

    public void AbleAnimator() 
    {
        mAnimator.enabled = true;
    }
    public void DesableAnimator() 
    {
        
        
        mAnimator.enabled = false;
        
    }
    public void EndAnimation() 
    {
        GDS.GestionEtatLance.CurrentState = GestionEtatLance.EtatLance.Idle;
        timeT = 0;
        GDS.CoupDepeeScript.ResetListEnnemies();
    }
    public void SetSens(bool right) 
    {
        mAnimator.SetBool("Right", right);
        
    }
    void getSens() 
    {
        
        SetSens(GestionScriptPlayer.MouvementGestion.Right);
    }
}
