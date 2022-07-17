using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoupDepee : MesFonctions
{
    public float SimpleDegat;
    public float EstocDegat;
    public float throwDegat;
    public enum Attaque 
    {
    Simple,
    Estoc

    }
    public Attaque LAttaque;
    List<GameObject> EnnemiesPdtCoup = new List<GameObject>();

    GestionScriptsLance LesScriptsLances;
    void Start()
    {
        LesScriptsLances = gestionLance(this.transform);
    }
    public void setState(Attaque MonAttaqueType) 
    {

        LAttaque = MonAttaqueType;

    }

    public void AddEnnemies(GameObject ennemie) 
    {
        
        bool IsInList = false;
        if (EnnemiesPdtCoup.Count!=0)
        {
            
            foreach (GameObject item in EnnemiesPdtCoup)
            {
                if (item == ennemie)
                {
                    IsInList = true;
                }
            }
        }
        if (!IsInList)
        {
            
            EnnemiesPdtCoup.Add(ennemie);
        }
        if (ennemie!=null && !IsInList )
        {
            
            DealDommage(ennemie);
        }
    }

    public void ResetListEnnemies()
    {
        EnnemiesPdtCoup = new List<GameObject>();
    }

    void DealDommage(GameObject ennemie) 
    {
        EnnemieLife Life = ennemie.GetComponent<EnnemieLife>();
        if (LAttaque == Attaque.Simple)
        {
            Life.RetireVie(SimpleDegat);
        }
        if (LAttaque == Attaque.Estoc)
        {
            Life.RetireVie(EstocDegat);
        }
    }

    public void ThrowedOnEnnemie(GameObject ennemie) 
    {
        EnnemieLife Life = ennemie.GetComponent<EnnemieLife>();
        Life.RetireVie(throwDegat);
    }


}
