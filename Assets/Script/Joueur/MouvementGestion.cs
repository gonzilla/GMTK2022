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

    [SerializeField] float ToleranceUnder;
    [SerializeField] float TempsImobilisationAtterrir;
    float Direction;
    float vitesseActuel;
    Rigidbody2D Rb;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        vitesseActuel = VitessePersoPdtMarche;
    }
    private void Update()
    {

        if (EtatDeDeplacement!= MesEtats.Tombe && EtatDeDeplacement!=MesEtats.Saute)
        {
            Rb.velocity = new Vector3(Direction*vitesseActuel,Rb.velocity.y,0);
        }

    }
    public void SetDirection(float newDirection ) 
    {
        if (EtatDeDeplacement == MesEtats.Immobile && newDirection!=0)
        {
            SetState(MesEtats.Marche);
        }
        Direction = newDirection;

    }
    #region state
    public void SetState(MesEtats NewState) 
   {
        if (IsGrounded)
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
        Rb.AddForce(Vector2.up *ForceSaut);
        IsGrounded = false;
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
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position - new Vector3(0.5f, ToleranceUnder,0), transform.position - new Vector3(-0.5f, ToleranceUnder, 0));
    }
}
