using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MesFonctions
{
    [SerializeField] Animator ShieldAnimator;
    //[SerializeField] Transform shield;
    GestionScript GS;
    public float speedShield;
    Collider2D monColl;
    SpriteRenderer ShieldSprite;
    
    void Start()
    {
        GS = FindGestionScript(Datas.LesDatas.Player);
        monColl = GetComponent<Collider2D>();
        ShieldAnimator.SetFloat("SpeedBlockage", speedShield);
        ShieldSprite = ShieldAnimator.transform.GetComponent<SpriteRenderer>();
    }

    public void StartDefense() 
    {
        if (GS.MouvementGestion.EtatDeDeplacement != MouvementGestion.MesEtats.Tombe)
        {
            Datas.LesDatas.Defending = true;
            monColl.enabled = true;
            ShieldAnimator.SetTrigger("ActiveBlock");
            GS.MouvementGestion.SetDirection(0);
            if (GS.ScriptsLance.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.PreparationThrow)
            {
                GS.ScriptsLance.ThrowLanceScript.PrepareThrow();
                GS.ScriptsLance.RotationLance.ResetRotation();
            }
            ShieldSprite.sortingOrder = 1;
        }
        
    }
    public void EndBlock() 
    {
        monColl.enabled = false;
        Datas.LesDatas.Defending = false;
        ShieldAnimator.SetTrigger("DesactiveBlock");
        GS.MouvementGestion.SetDirection(GS.MouvementGestion.GetDirectionV());
        ShieldSprite.sortingOrder = -1;
    }
    public void setDirection(bool Right) 
    {
        
        if (Right)
        {
            this.transform.localScale = new Vector3(1,1,1);
        }
        if (!Right) 
        {
            this.transform.localScale = new Vector3(-1, 1, 1);

        }
    }
    
}
