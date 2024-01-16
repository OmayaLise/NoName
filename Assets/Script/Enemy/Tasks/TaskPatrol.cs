using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TaskPatrol : Node
{
    private Transform transform;
    List<Transform> waypoints;
    float waitCounter;
    float waitingTime = 4f;
    float speed = 2f;
    bool waiting;
    int currentWaypointIndex;
    Rigidbody2D rb;   
    Animator animator;
    private Vector2 lastFacingDirection = Vector2.right;

    public TaskPatrol(Transform transformGiven, List<Transform> waypointsGiven, Rigidbody2D rbGiven, Animator animatorGiven)
    {
        transform = transformGiven;
        waypoints = waypointsGiven;
        rb = rbGiven;
        animator = animatorGiven;   
    }
    public override NodeState EvaluateState()
    {
        if(waiting)
        {
            rb.velocity.Set(0f, rb.velocity.y);
            animator.SetBool("isRunning", false);

            waitCounter += Time.deltaTime;
            if(waitCounter >= waitingTime)
            {
                waiting = false; 
            }
        }
        else
        {
            Transform currentWaypoint = waypoints[currentWaypointIndex];

            if(Vector2.Distance(transform.position, currentWaypoint.position) < 0.5f)
            {
                waitCounter = 0f;   
                waiting = true;
                currentWaypointIndex = ( currentWaypointIndex + 1) %  waypoints.Count;
            }
            else
            {
                Vector2 direction = currentWaypoint.position - transform.position;
                direction.Normalize();
                rb.velocity = direction * speed;
                StartRun();
            }
        }
        state = NodeState.RUNNING;
        return state;   
    }

    void StartRun()
    {
        if (rb != null && animator != null && rb.velocity.x < 0.1f)
        {
            animator.SetBool("isRunning", true);
            UpdateFacingDirection(Vector2.right);
        }
        else if (rb != null && animator != null && rb.velocity.x > 0.1f)
        {
            animator.SetBool("isRunning", true);

            UpdateFacingDirection(Vector2.left);

        }


    }

    private void UpdateFacingDirection(Vector2 direction)
    {
        // Check if there's a significant change in the horizontal direction
        if (Mathf.Abs(direction.x) > Mathf.Epsilon)
        {
            if ((direction.x < 0 && lastFacingDirection.x > 0) || (direction.x > 0 && lastFacingDirection.x < 0))
            {
                // Flip the sprite by multiplying the x local scale by -1
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;

                // Update the last facing direction
                lastFacingDirection = direction;
            }
        }
    }
}
