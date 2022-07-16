using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLance : MesFonctions
{

    public enum StateSword 
    {
    Epee,
    Lance,
    Platform
    }
    public StateSword MonEtat;
    public bool Throwed;

    public float SpeedRotationLance;

    Camera maincam;

    private void Start()
    {
        maincam = Datas.LesDatas.MainCam;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Direction = maincam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, SpeedRotationLance * Time.deltaTime);
        
       
    }

    public void Throw() 
    {
    


    }
}
