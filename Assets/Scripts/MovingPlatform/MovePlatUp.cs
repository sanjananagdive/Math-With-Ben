using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatUp : MonoBehaviour
{
    public float speed;
    public Transform waypoint0;
    public Transform waypoint1;

    private bool platCanMove= false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered platform collider.");
            platCanMove=true;
            col.transform.SetParent(transform);
            InvokeRepeating("MoveUp", 2f, 0.1f);
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            platCanMove= false;
            col.transform.SetParent(null);
            CancelInvoke("MoveUp"); // Cancel the invoke if the player hops off
        }
    }

    public void MoveUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoint1.position, speed * Time.deltaTime);

    }
}
