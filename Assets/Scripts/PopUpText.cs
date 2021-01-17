using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour
{
    public Text text;

    public GameObject korb;

    public string content; // Wird beim Instanziieren festgelegt

    public Color textColor; // Wird beim Instanziieren festgelegt

    void Start()
    {
        korb = GameObject.Find("Korb"); // Spielerobjekt finden und speichern
        text.text = content; // Text festlegen
        text.color = textColor; // Textfarbe festlegen
        transform.position = korb.transform.position; // Position des Spielers festlegen
    }

    void Update()
    {
        transform.position = transform.position + Vector3.up/180; // Aufwärtsbewegung des Textes
    }
}
