using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Vector3 Decalage;
    [SerializeField] float SpeedCam;
    Vector3 PlyerPos;
    Vector3 PositionVoulue;

    void Start()
    {
        Player = Datas.LesDatas.Player;
    }

    // Update is called once per frame
    void Update()
    {
        PlyerPos = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        PositionVoulue = PlyerPos + Decalage;
        transform.position = Vector3.Lerp(transform.position, PositionVoulue, SpeedCam * Time.deltaTime);
    }
}
