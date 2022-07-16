using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesFonctions : MonoBehaviour
{
   public static GestionScript FindGestionScript(Transform Player) 
   {
        return Player.GetComponent<GestionScript>();
   }

    public static Transform GetChildByName(string Name, Transform Parent)
    {

        Transform Elu = null;
        foreach (Transform enfant in Parent)
        {
            if (Name == enfant.name)
            {
                Elu = enfant;
                break;
            }
        }
        if (Elu == null)
        {
            print("attention je ne trouve pas d'enfant de ce nom, je re");
        }
        return Elu;
    }// vas chercher un enfant


    public static Vector3 directionBetweenTwoPoint(Vector3 Origin, Vector3 Point)
    {


        return (Point - Origin).normalized;

    }
}
