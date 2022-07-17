using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceAttaque : MesFonctions
{
    public float TimeMaxAttaque;
    public float TimeMaxForEstoc;
    GestionScriptsLance GDS;
    [SerializeField] Animator mAnimator;
    public List<EnnemieLife> mesEnnemies = new List<EnnemieLife>();
    public float animationSpeedAttSimp;
    public float animationSpeedEstoc;
    bool maintient;
    // Start is called before the first frame update
    void Start()
    {
        GDS = gestionLance(this.transform);
        mAnimator = GetComponent<Animator>();
        mAnimator.enabled = false;
        CalibrageAnimation();
    }

    public void determineAttack(bool maint) 
    {
        if (GDS.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.Idle)
        {
            maintient = maint;
        }
       
    
    }
     void Update()
    {
        
    }
    void AttaqueSimple() 
    {
        GDS.GestionEtatLance.CurrentState = GestionEtatLance.EtatLance.Attacking;
        mAnimator.enabled = true;
        mAnimator.SetTrigger("AttaqueSimple");
        Invoke("DesableAnimator", mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }
     void Estoc() 
    {
        GDS.GestionEtatLance.CurrentState = GestionEtatLance.EtatLance.Attacking;
        mAnimator.enabled = true;
        mAnimator.SetTrigger("Estoc");
        Invoke("DesableAnimator", mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }
    void CalibrageAnimation() 
    {

        mAnimator.SetFloat("AttaqueSimpleSpeed", animationSpeedAttSimp);
        mAnimator.SetFloat("EstocSpeed", animationSpeedEstoc);
    }

    void DesableAnimator() 
    {
        mAnimator.enabled = false;
    }
}
