using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_szene : MonoBehaviour
{
    public float lookTime=1f;
    private float timer;
    private bool gazedAt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gazedAt){
            Debug.Log("Gazed at true");
        }
    }
    public void PointerEnter(){
        gazedAt=true;
    }
    public void PointerExit(){
gazedAt=false;
    }
}
