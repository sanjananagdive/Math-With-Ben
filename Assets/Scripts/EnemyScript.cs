using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Transform[] waypoints; // Array of waypoints to move between
    public float speed = 2f;      // Movement speed of the enemy
    private Animator anim;
    private bool move=true;

    private int currentWaypointIndex = 0; // Index of the current waypoint
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(move)
        {
            if (waypoints.Length == 0)
            return;

            MoveToWaypoint();
        }
    }

    void MoveToWaypoint()
    {
        // Get the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Move towards the current waypoint
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Check if the enemy has reached the waypoint
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Move to the next waypoint, looping back to the start if at the end
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        FlipSprite(targetWaypoint.position);

    }

    void FlipSprite(Vector2 targetPosition)
    {
        // Calculate the direction of movement
        Vector2 direction = targetPosition - (Vector2)transform.position;

        // Flip the sprite based on the direction of movement
        if (direction.x > 0 && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0 && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            move = false;
            Debug.Log("Hit Player");
            anim.SetBool("Attack", true);

        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            move= true;
            anim.SetBool("Attack", false);
            anim.SetTrigger("Walk");

        }
    }
}
