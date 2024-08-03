using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    public static DetectionScript instance;

    public bool playerEntered = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            playerEntered = true;
            print("Player entered");
        }
    }
}
