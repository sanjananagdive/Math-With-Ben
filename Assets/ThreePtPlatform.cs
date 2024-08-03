using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePtPlatform : MonoBehaviour
{
    public float speed;
    public Transform waypoint0;
    public Transform waypoint1;
    public float delay = 0.5f;
    private bool startMoving=false;
    private bool platCanMove=false;

    private bool playerOnPlatform = false;

    private bool myBool;
    public GameObject eq2;

    int currentWaypointIndex;

    private bool isMoving = false;

    public static ThreePtPlatform instance;

    void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        myBool = EquationScript.instance.setPlatformActive;
        // Instantiate the prefab at the specified position and rotation when the space key is pressed
        if (myBool)
        {
            if(eq2==null)
            {
                Invoke("MoveToZero", 1f);
    
                myBool=false;
            }
            
        }
    } 

    

     

}
