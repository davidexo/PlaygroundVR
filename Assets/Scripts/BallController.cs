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
    public GameObject popUpPrefab = null;
    private int punkte = 0;
    private Vector3 startPoint; // Veriable um den Startpunkt des Balles zu speichern

    bool thrown = false;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = ball.transform.position; // Startpunkt des Balls speichern
    }

    // Update is called once per frame
    void Update()
    {
        // Ball wird vor Kamera platziert und der Timer gestartet
        if(isGrabbed)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().Sleep();
            transform.position = playerCamera.transform.position + playerCamera.transform.forward * ballOffset;
            timerStarted = true;
            thrown = false; 
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

    public void popUp(string text, Color color){ // PopUpText erstellen und anzeigen lassen
        GameObject popUp = Instantiate(popUpPrefab); // PopUp Objekt instantiieren und speichern
        popUp.GetComponent<PopUpText>().content = text; // Text des PupUps anpassen an Übergabeparameter
        popUp.GetComponent<PopUpText>().textColor = color; // Textfarbe anpassen an Übergabeparameter
        Destroy(popUp, 5); // Verschwindenlassen des Textes nach 5 Sekunden
    }

    void OnTriggerEnter(Collider other) // Kollisionen feststellen
    {
        if (other.gameObject.CompareTag ("Korb")) // Tag des kollidierten Objekt mit Korb vergleichen
        {
            if(!thrown){ // Prüfen ob schon geworfen wurde, da sonst mehrere PopUpTexte auf einmal erstellt werden
                punkte++; // Punktzahl erhöhen
                popUp("Punkte\n" + punkte.ToString(), Color.black); // Aufruf um ein PopUpText zu erstellen
                thrown=true; // festhalten dass schon geworfen wurde
            }
        }

        if (other.gameObject.CompareTag ("BasketballBereich")) // Tag des kollidierten Objekt mit BasketballBereich vergleichen
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0,0,0); // Beschleunigung deaktivieren
            ball.transform.position = startPoint; // Ball zurück auf gespeicherten Startpunkt setzen
        }
    }
}
