using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lifeplayer : MesFonctions
{
    GestionScript GS;
    public float Life = 100;
    void Start()
    {
        GS = FindGestionScript(this.transform);
    }

    // Update is called once per frame
    public void TakeDommage(float Dommage) 
    {

        Life -= Dommage;
        if (Life<=0)
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }
}
