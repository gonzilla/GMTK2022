using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieIA : MesFonctions
{
    public enum StateIA 
    {
        Vadrouille,
        Pourchasse,
        Attaque,
    }
    public StateIA MonEtatCourant;
    public float SpeedBetweenPoints;
    public float rangeDetection;
    public float DistanceAttaque;
    public float DistanceSnapPoint;
    public float DammageContact;
    public float TimeTrigger;
    public Transform[] PointsDeVadrouille;
    public Transform RaycastDetectionOriginPosition;
    Vector3 DirectionNextPoint;

    int SensPoints = 1;
    int Indexpoint =1;
    GestionsSciptsEnnemies GSE;
    CircleCollider2D MonCollider;

    void Start()
    {
        GSE = GetEnnemieGestion(this.transform);
        ChooseMyNextPoint();
        //CalculSpeedEtDirection()
        MonCollider = GetComponent<CircleCollider2D>();
    }

    void ChooseMyNextPoint() 
    {
        if (PointsDeVadrouille.Length>0)
        {
            Indexpoint += SensPoints;
            CheckOrder();
            CalculSpeedEtDirection();
        }
        
    }
    void CalculSpeedEtDirection() 
    {
        DirectionNextPoint = (PointsDeVadrouille[Indexpoint].position - transform.position);
    }

    void CheckOrder() 
    {
        if (Indexpoint == PointsDeVadrouille.Length-1 || Indexpoint == 0)
        {
            SensPoints *= -1;
        }
    
    }
    void Update()
    {
        if (MonEtatCourant== StateIA.Vadrouille)
        {
            transform.position += DirectionNextPoint.normalized * SpeedBetweenPoints * Time.deltaTime; ;
            if (transform.position == PointsDeVadrouille[Indexpoint].position || Vector3.Distance(transform.position, PointsDeVadrouille[Indexpoint].position)< DistanceSnapPoint)
            {
                transform.position = PointsDeVadrouille[Indexpoint].position;
                ChooseMyNextPoint();
            }
        }
        
    }

    void backPreviousPoint() 
    {
        Indexpoint += -SensPoints;
        if (Indexpoint<=0)
        {
            Indexpoint = 1;
            SensPoints = -1;
        }
        if (Indexpoint >= PointsDeVadrouille.Length-1)
        {
            Indexpoint = PointsDeVadrouille.Length - 1;
            SensPoints = 1;
        }
        CalculSpeedEtDirection();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Shield"))
        {
            backPreviousPoint();
            MonCollider.isTrigger = true;
            if (collision.transform.CompareTag("Player"))
            {
                collision.transform.GetComponent<lifeplayer>().TakeDommage(DammageContact);
            }
            
            Invoke("ResetCollider", TimeTrigger);
        }
    }

    void ResetCollider()
    {
        MonCollider.isTrigger = false; 
    }
}
