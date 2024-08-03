using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformScript : MonoBehaviour
{
    public float speed;
    public Transform waypoint0;
    public Transform waypoint1;
    public float delay = 0.5f;
    private bool startMoving=false;
    private bool platCanMove=false;
    //private bool playerDropped=false;


    
    private bool playerOnPlatform = false;
   // private bool delayStarted = false;

    public static MovePlatformScript instance;

    void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(EquationScript.instance.movePlatform)
        {
            Invoke("MovePlatformLeft", 2f);
        }*/
        if(EquationScript.instance.movePlatform)
        {
            startMoving=true;
            platCanMove= true;
        }

        if(playerOnPlatform)
        {
            Invoke("StartMovingTowardsWaypoint1", 1.5f);  

        }
        else if(startMoving && platCanMove && !playerOnPlatform)
        {
            Invoke("StartMovingTowardsWaypoint0", 1.3f);
        }

        if(playerOnPlatform)
        {
            platCanMove=false;
        }
    }

    void MoveTowardsWaypoint(Transform targetWaypoint)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            
            Debug.Log("Player entered platform collider.");
            playerOnPlatform = true;
            col.transform.SetParent(transform);
            Invoke("StartMovingTowardsWaypoint1", 9f); // Invoke after delay seconds
        }
        
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            startMoving=false;
            platCanMove= false;
            col.transform.SetParent(null);
            CancelInvoke("StartMovingTowardsWaypoint1"); // Cancel the invoke if the player hops off
            CancelInvoke("StartMovingTowardsWaypoint0");
        }
    }
    void StartMovingTowardsWaypoint1()
    {
        MoveTowardsWaypoint(waypoint1);
    }

    void StartMovingTowardsWaypoint0()
    {
        MoveTowardsWaypoint(waypoint0);
    }

}
