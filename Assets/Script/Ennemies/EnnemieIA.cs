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
    public Transform[] PointsDeVadrouille;
    public Transform RaycastDetectionOriginPosition;
    Vector3 DirectionNextPoint;

    int SensPoints = 1;
    int Indexpoint =1;
    GestionsSciptsEnnemies GSE;

    void Start()
    {
        GSE = GetEnnemieGestion(this.transform);
        ChooseMyNextPoint();
    }

    void ChooseMyNextPoint() 
    {
        if (PointsDeVadrouille.Length>0)
        {
            Indexpoint += SensPoints;
        }
        
    }
    void CalculSpeedEtDirection() 
    {
        DirectionNextPoint = PointsDeVadrouille[Indexpoint].position - transform.position, PointsDeVadrouille[Indexpoint].position;
    }

    void CheckOrder() 
    {
        if (Indexpoint == PointsDeVadrouille.Length-1 || Indexpoint == 0)
        {
            SensPoints *= -1;
        }
    
    }
}
