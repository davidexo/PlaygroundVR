using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject ball;
    public float ballOffset = 2f;
    public float throwForce = 6f;
    private bool isGrabbed = false;
    private float timer;
    private bool timerStarted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Ball wird vor Kamera platziert und der Timer gestartet
        if(isGrabbed)
        {
            GetComponent<Rigidbody>().useGravity = false;    
            transform.position = playerCamera.transform.position + playerCamera.transform.forward * ballOffset;
            timerStarted = true;
        }

        // Der Timer wird hochgezählt
        if(timerStarted)
        {
            StartCount();    
        }

        // Der Ball wird geworfen und alle Variablen zurückgesetzt
        if(timer > 2 && timerStarted == true)
        {
            ThrowBall();

            isGrabbed = false;
            timerStarted = false;
            timer = 0;
        }
        if(transform.position.y<=-1)
            transform.position=new Vector3(0,0,0);
    }

    public void GrabBall()
    {
        isGrabbed = true;
    }

    private void ThrowBall()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * throwForce);
    }

    private void StartCount()
    {
        timer += Time.deltaTime;
    }
}
