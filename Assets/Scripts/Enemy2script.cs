using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2script : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Transform[] waypoints; // Array of waypoints to move between
    public float speed = 2f;      // Movement speed of the enemy
    private Animator anim;
    private bool move=true;

    //private int currentWaypointIndex = 0; // Index of the current waypoint

    public bool moveTowardsWaypoint0= true;

    
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(move && !PlayerController.instance.gameover)
        {
            if (waypoints.Length == 0)
            return;

            MoveToWaypoint();
        }
    }

    void MoveToWaypoint()
    {
        // Get the current waypoint
        //Transform targetWaypoint = waypoints[currentWaypointIndex];
        Transform targetWaypoint = moveTowardsWaypoint0? waypoints[0]:waypoints[1];
        // Move towards the current waypoint
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Check if the enemy has reached the waypoint
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Move to the next waypoint, looping back to the start if at the end
            //currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            moveTowardsWaypoint0 = !moveTowardsWaypoint0;
        }
        FlipSprite(targetWaypoint.position);

    }

    void FlipSprite(Vector2 targetPosition)
    {
        // Calculate the direction of movement
        Vector2 direction = targetPosition - (Vector2)transform.position;

        // Flip the sprite based on the direction of movement
        if (direction.x > 0 && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x < 0 && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            //move = false;
            Debug.Log("Hit Player");
            //anim.SetBool("Hit", true);
            StartCoroutine("HitAnim");

            PlayerController.instance.TakeDamage(30);
           // UIManager.instance.DecreaseGemCount();

        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Banana"))
        {
            moveTowardsWaypoint0=!moveTowardsWaypoint0;
        }
    }

    IEnumerator HitAnim()
    {
        move = false;
        anim.SetBool("Hit", true);

        yield return new WaitForSeconds(2f);

        move = true;
        anim.SetBool("Hit", false);
    }
    
}
