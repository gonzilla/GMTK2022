using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLance : MesFonctions
{

    
    
    

    public float SpeedRotationLance;

    Camera maincam;

    GestionScriptsLance LesScriptsLances;

    Quaternion BaseRotation;
    void Start()
    {
        LesScriptsLances = gestionLance(this.transform);
        maincam = Datas.LesDatas.MainCam;
        BaseRotation = transform.rotation;
    }
    

    // Update is called once per frame
    void Update()
    {
        
        if (LesScriptsLances.GestionEtatLance.CurrentState == GestionEtatLance.EtatLance.PreparationThrow)
        {
            Vector2 Direction = maincam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, SpeedRotationLance * Time.deltaTime);
        }
        
        
        
       
    }
    public void ResetRotation() 
    {
        transform.rotation = BaseRotation;
    
    }

   
    
}
