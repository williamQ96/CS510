using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;

    bool m_IsPlayerInRange;

    void OnTriggerEnter (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    // To visulize a cone showing the ghost's sight for debugging
    void OnDrawGizmos()
    {
        if (player == null) return;

        Vector3 forward = transform.forward;
        Vector3 toPlayer = (player.position - transform.position).normalized;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up, forward * 5f);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + Vector3.up, toPlayer * 5f);

        // Draw view cone boundaries
        Gizmos.color = Color.yellow;
        float viewAngle = 60f; // degrees
        Quaternion leftRayRotation = Quaternion.AngleAxis(-viewAngle / 2, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(viewAngle / 2, Vector3.up);

        Vector3 leftRay = leftRayRotation * forward;
        Vector3 rightRay = rightRayRotation * forward;

        Gizmos.DrawRay(transform.position + Vector3.up, leftRay * 5f);
        Gizmos.DrawRay(transform.position + Vector3.up, rightRay * 5f);
    }

    void Update ()
    {
        // Use dot product to determine if the ghost is looking toward the player
        // +1: same direction, 0: perpendicular, -1: opposite direction
        if (m_IsPlayerInRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized; // Calculate the normalized direction
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer); // Compute dot product

            if (dotProduct > 0.3f) // If dot product roughly  > 72 degrees, player is in front of ghost
            {
                Vector3 rayOrigin = transform.position + Vector3.up;
                Ray ray = new Ray(rayOrigin, directionToPlayer);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) // Perform a raycast to check if there's a clear line
                {
                    if (hit.transform == player) // Confirm the ray hit the player before triggering game ending
                    {
                        gameEnding.CaughtPlayer();
                    }
                }
            }
        }

        // if (m_IsPlayerInRange)
        // {
        //     Vector3 direction = player.position - transform.position + Vector3.up;
        //     Ray ray = new Ray(transform.position, direction);
        //     RaycastHit raycastHit;
            
        //     if (Physics.Raycast (ray, out raycastHit))
        //     {
        //         if (raycastHit.collider.transform == player)
        //         {
        //             gameEnding.CaughtPlayer ();
        //         }
        //     }
        // }
    }
}
