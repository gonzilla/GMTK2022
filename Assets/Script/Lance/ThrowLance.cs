using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLance : MesFonctions
{
    public float ForceThrow;
    public float ForceBack;
    public float RangeMax;
    public float DelayPlant;
    [SerializeField] float snap;

    Vector3 DirectionThrow;
    Vector3 DirectionBack;
    
    GestionScriptsLance LesScriptsLances;
    GestionScript LesScriptJoueurs;
    void Start()
    {
        LesScriptsLances = gestionLance(this.transform);
        LesScriptJoueurs = FindGestionScript(Datas.LesDatas.Player);
    }

    
    public void PrepareThrow() 
    {
        
        if (LesScriptsLances.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.Idle && !Datas.LesDatas.Defending)
        {
            LesScriptsLances.GestionEtatLance.SetState(GestionEtatLance.EtatLance.PreparationThrow);
            transform.position = Datas.LesDatas.ThrowParent.position;
            transform.parent = Datas.LesDatas.ThrowParent;
            LesScriptsLances.LesAttaques.DesableAnimator();
        }
        else if (LesScriptsLances.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.PreparationThrow)
        {
            LesScriptsLances.GestionEtatLance.SetState(GestionEtatLance.EtatLance.Idle);
            transform.position = Datas.LesDatas.CombatParent.position;
            transform.parent = Datas.LesDatas.CombatParent;
            LesScriptsLances.LesAttaques.DesableAnimator();
        }
        else if (LesScriptsLances.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.Throwed
            || LesScriptsLances.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.InWall)
        {
            back();
        }


    }
    public void Throw()
    {
        if (LesScriptsLances.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.PreparationThrow)
        {
            LesScriptsLances.GestionEtatLance.SetState(GestionEtatLance.EtatLance.Throwed);
            transform.parent = null;
            DirectionThrow = transform.right.normalized;
            Rigidbody2D rb = this.transform.gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
       

    }
    void back() 
    {
        if (LesScriptsLances.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.Throwed || LesScriptsLances.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.InWall)
        {
            if (LesScriptsLances.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.InWall)
            {

                BoxCollider2D temp = this.transform.GetChild(0).GetComponent<BoxCollider2D>();
                temp.usedByEffector = false;
                temp.isTrigger = true;
                Destroy(this.transform.GetChild(0).GetComponent<PlatformEffector2D>());
                this.transform.gameObject.tag = "Sword";
            }
            LesScriptsLances.GestionEtatLance.SetState(GestionEtatLance.EtatLance.BackingThrow);
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            
        }

    }

    public void StartstuckInAWall() 
    {
        

        Invoke("stuckInWALL", DelayPlant/10);
    }
    void stuckInWALL() 
    {
        LesScriptsLances.GestionEtatLance.SetState(GestionEtatLance.EtatLance.InWall);
        BoxCollider2D temp = this.transform.GetChild(0).GetComponent<BoxCollider2D>();
        temp.usedByEffector = true;
        temp.isTrigger = false;
        this.transform.GetChild(0).gameObject.AddComponent<PlatformEffector2D>();
        this.transform.gameObject.tag = "Platform";


    }

    public void TouchAnEnnemie(GameObject Ennemie) 
    {
        LesScriptsLances.CoupDepeeScript.ThrowedOnEnnemie(Ennemie);
        back();
    }
    void setAnimator() 
    {
        LesScriptsLances.LesAttaques.AbleAnimator();
        LesScriptsLances.LesAttaques.SetSens(LesScriptJoueurs.MouvementGestion.Right);
    }

    private void Update()
    {
        if (LesScriptsLances.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.Throwed)
        {
            transform.position += new Vector3(DirectionThrow.x, DirectionThrow.y, 0) * ForceThrow * Time.deltaTime;
            if (Vector3.Distance(transform.position,Datas.LesDatas.Player.position)>RangeMax)
            {
                back();
            }
        }
        if (LesScriptsLances.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.BackingThrow)
        {
            DirectionBack =   Datas.LesDatas.CombatParent.position - transform.position;
            transform.position += DirectionBack.normalized * ForceBack * Time.deltaTime;
            if (Vector3.Distance(transform.position,Datas.LesDatas.Player.position)<(DirectionBack.normalized* ForceBack *Time.deltaTime).magnitude+ snap)
            {
                transform.position = Datas.LesDatas.CombatParent.position;
                LesScriptsLances.GestionEtatLance.SetState(GestionEtatLance.EtatLance.Idle);
                transform.parent = Datas.LesDatas.CombatParent;
                LesScriptsLances.RotationLance.ResetRotation();
                setAnimator();
            }
        }
    }
    

}
