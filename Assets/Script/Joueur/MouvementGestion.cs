using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouvementGestion : MesFonctions
{
    public enum MesEtats 
    {
    Marche,
    Cours,
    Saute,
    Tombe,
    Immobile
    }
    public MesEtats EtatDeDeplacement;
    public bool IsGrounded;
    public float VitessePersoPdtMarche;
    public float VitessePersoPdtCourse;
    public float ForceSaut;
    public bool Right;
    [SerializeField] float ToleranceUnder;
    [SerializeField] float TempsImobilisationAtterrir;
    [SerializeField] GameObject PlatformCurrent;
    PlatformScript forreset;
    float Direction;
    float DirectionVoulue;
    float vitesseActuel;
    Rigidbody2D Rb;
    GestionScript MaGestion;
    SpriteRenderer MonSprite;
    Vector3 Hauteur;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        vitesseActuel = VitessePersoPdtMarche;
        MaGestion = FindGestionScript(this.transform);
        MonSprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {

        if (EtatDeDeplacement!= MesEtats.Tombe && EtatDeDeplacement!=MesEtats.Saute)
        {
            Rb.velocity = new Vector3(Direction*vitesseActuel,Rb.velocity.y,0);
        }
        if (EtatDeDeplacement == MesEtats.Saute && Hauteur.y>transform.position.y)
        {
            SetState(MesEtats.Tombe);
        }
        else if (EtatDeDeplacement == MesEtats.Saute && Hauteur.y < transform.position.y)
        {
            Hauteur = transform.position;
        }

    }
    public void SetDirection(float newDirection ) 
    {
        if (!Datas.LesDatas.Defending )
        {
            if (EtatDeDeplacement == MesEtats.Immobile && newDirection != 0)
            {
                SetState(MesEtats.Marche);
            }
            if (newDirection == 1)
            {
                Right = true;
                MonSprite.flipX = false;
            }
            else if (newDirection == -1)
            {
                MonSprite.flipX = true;
                Right = false;
            }
            MaGestion.GestionDushield.shield.setDirection(Right);
            Direction = newDirection;
        }
        if (Datas.LesDatas.Defending )
        {
            Direction = 0;
            DirectionVoulue = newDirection;
        }

        

    }
    public float GetDirectionV() 
    {
        return DirectionVoulue;
    }
    public void PassThroughPlatform() 
    {
        if (PlatformCurrent!=null)
        {
            PlatformEffector2D maPlat = PlatformCurrent.GetComponent<PlatformEffector2D>();
            maPlat.rotationalOffset = 180;
        }
        
        
    }
    #region state
    public void SetState(MesEtats NewState) 
   {
        
            EtatDeDeplacement = NewState;
            switch (NewState)
            {
                case MesEtats.Marche:
                    marche();
                    break;

                case MesEtats.Cours:
                    cours();
                    break;

                case MesEtats.Saute:
                    saute();
                    break;

                case MesEtats.Tombe:
                    Tombe();
                    break;

                case MesEtats.Immobile:
                    Immobile();
                    break;
                
            }
        

   }

    void marche() 
    {
        vitesseActuel = VitessePersoPdtMarche;
    }

    void cours() 
    {
        vitesseActuel = VitessePersoPdtCourse;
    }

    void saute() 
    {
        if (!Datas.LesDatas.Defending)
        {
            Rb.AddForce(Vector2.up * ForceSaut);
            IsGrounded = false;
            Hauteur = transform.position;
        }
        
    }
    void Tombe() 
    {
    
    
    }
    void Immobile() 
    {
        vitesseActuel = 0;
        if (Direction!=0)
        {
            Invoke("marche", TempsImobilisationAtterrir);
        }

    }


    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).point.y<transform.position.y-ToleranceUnder)
        {
            IsGrounded = true;
            SetState(MesEtats.Immobile);
            if (collision.transform.CompareTag("Platform"))
            {
                PlatformCurrent = collision.transform.gameObject;
                if (PlatformCurrent.transform == Datas.LesDatas.Lance)
                {
                    PlatformCurrent = PlatformCurrent.transform.GetChild(0).gameObject;
                }
                if (!PlatformCurrent.GetComponent<PlatformScript>())
                {
                    forreset = PlatformCurrent.transform.gameObject.AddComponent<PlatformScript>();
                }
                else 
                {
                    forreset = PlatformCurrent.GetComponent<PlatformScript>();
                }
               
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.transform.CompareTag("Platform"))
        {
            PlatformCurrent = null;
            if (forreset!=null)
            {
                forreset.resetMe();
                forreset = null;
            }
           
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position - new Vector3(0.5f, ToleranceUnder,0), transform.position - new Vector3(-0.5f, ToleranceUnder, 0));
    }
}
