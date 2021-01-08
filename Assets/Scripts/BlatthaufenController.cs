using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlatthaufenController : MonoBehaviour
{   

    private GameObject blatt;
    private float timer;
    private bool startCount = false;

    void Start ()
    {
        blatt = GameObject.Find("UI-image-leafs");
        blatt.GetComponent<Renderer>().enabled=false;
    }

    void Update()
    {
        if(startCount == true)  
        {
            timer += Time.deltaTime;
        }

        if(timer > 2)
        {
            blatt.GetComponent<Renderer>().enabled=false;
            
            startCount = false;
            timer = 0;
        }
    }
    void OnTriggerEnter(Collider col){
     
        blatt.GetComponent<Renderer>().enabled=true;
        startCount = true;
}
}