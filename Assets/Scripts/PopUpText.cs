using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour
{
    public Text text; // Wird beim Instanziieren festgelegt

    public GameObject korb; // Wird beim Instanziieren festgelegt

    public string content; // Wird beim Instanziieren festgelegt

    public Color textColor; // Wird beim Instanziieren festgelegt

    void Start()
    {
        korb = GameObject.Find("Korb"); // Spielerobjekt finden und speichern um die Position des Textes festzulegen
        text.text = content; // Text festlegen, welcher angezeigt werden soll
        text.color = textColor; // Textfarbe festlegen
        transform.position = korb.transform.position; // Position des Korbes festlegen
    }

    void Update()
    {
        transform.position = transform.position + Vector3.up/180; // Aufwärtsbewegung des Textes
    }
}
