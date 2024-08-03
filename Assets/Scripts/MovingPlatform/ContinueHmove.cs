using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueHmove : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    private int currentwaypointIndex=0;
    private bool movingForward=true;
    private bool threePtPlatform=false;
    public GameObject eq2;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.CompareTag("ThreePtPlatform"))
        {
            threePtPlatform= true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Vector2.Distance(transform.position, waypoints[currentwaypointIndex].position)<0.2f)
        {
            if(movingForward)
            {
                currentwaypointIndex++;
                if(currentwaypointIndex==waypoints.Length)
                {
                   currentwaypointIndex=currentwaypointIndex-1;
                   movingForward=!movingForward;
                }
            }

            else if(!movingForward)
            {
                currentwaypointIndex--;
                if(currentwaypointIndex<0)
                {
                    currentwaypointIndex=0;
                    movingForward= true;
                }
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentwaypointIndex].position, speed* Time.deltaTime);
        */
        if(!threePtPlatform)
        {
            StartMoving();
        }
        else if(threePtPlatform)
        {
            if(eq2==null)
            {
                StartMoving();
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            
            Debug.Log("Player entered platform collider.");
            col.transform.SetParent(transform);
            //Invoke("StartMovingTowardsWaypoint1", 9f); // Invoke after delay seconds
        }
        
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(null);
        }
    }

    public void StartMoving()
    {
        if(Vector2.Distance(transform.position, waypoints[currentwaypointIndex].position)<0.2f)
        {
            if(movingForward)
            {
                currentwaypointIndex++;
                if(currentwaypointIndex==waypoints.Length)
                {
                   currentwaypointIndex=currentwaypointIndex-1;
                   movingForward=!movingForward;
                }
            }

            else if(!movingForward)
            {
                currentwaypointIndex--;
                if(currentwaypointIndex<0)
                {
                    currentwaypointIndex=0;
                    movingForward= true;
                }
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentwaypointIndex].position, speed* Time.deltaTime);
    }
}
