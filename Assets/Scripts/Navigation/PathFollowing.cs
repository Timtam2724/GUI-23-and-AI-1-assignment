using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GGL;

public class PathFollowing : Steering {

    #region Path Following Variables
    public Transform target; // Tells the NPC where to go
    public float nodeRadius = .1f;
    public float targetRadius = 3f;

    private int currentNode = 0; // Keep track of the individual nodes
    private bool isAtTarget = false; // Starts the script with the NPC thinking they are not at the target
    private NavMeshAgent nav; // Reference to the NavMeshAgent iside the NPC
    private NavMeshPath path; // Stores the path the NPC has calulated to get to the target
    #endregion

    #region Start
    private void Start()
    {
        // Grabs the NavMeshAgent
        nav = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }
    #endregion

    #region Seek Target
    Vector3 Seek(Vector3 target)
    {
        Vector3 force = Vector3.zero;
        // Tells the NPC to go to the target
        Vector3 desiredForce = target - transform.position;
        float distance = isAtTarget ? targetRadius : nodeRadius;
        if(desiredForce.magnitude > distance)
        {
            desiredForce = desiredForce.normalized * weighting;
            force = desiredForce - owner.velocity;
        }
        // Return force
        return force;
    }
    #endregion

    #region On Draw Gizmos
    // Draws out the path the NPC follows
    void OnDrawGizmos()
    {
        // The color of the Gizmos will be red
        Gizmos.color = Color.red;
        // what to do if the path is not calculated
        if(path != null)
        {
            Vector3[] corners = path.corners;
            if(corners.Length > 0)
            {
                Vector3 targetPos = corners[corners.Length - 1];
                GizmosGL.color = new Color(1, 0, 0, 0.3f);
                GizmosGL.AddSphere(targetPos, targetRadius);
                // Calculate the distance from the agent to the target
                float distance = Vector3.Distance(transform.position, targetPos);
                // Checks if the distance is greater or equal to the target Radius
                if(distance >= targetRadius)
                {
                    Gizmos.color = Color.cyan;
                    // Draw lines between nodes
                    for (int i = 0; i < corners.Length - 1; i++)
                    {
                        Vector3 nodeA = corners[i];
                        Vector3 nodeB = corners[i + 1];
                        GizmosGL.AddLine(nodeA, nodeB, 0.1f, 0.1f);
                        GizmosGL.AddSphere(nodeB, 1f);
                        GizmosGL.color = Color.red;
                    }
                }                
            }
        }
    }
    #endregion
    #region Get Force
    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;

        // What to fo if there is not target
        if (!target)
        {
            return force;
        }
        
        // Calculate path using the nav agent
        if(nav.CalculatePath(target.position, path))
        {
            // What happends when the path to the target is calculated
            if(path.status == NavMeshPathStatus.PathComplete)
            {
                // Corners are the nodes the NPC travels throughout the level
                Vector3[] corners = path.corners;
                // Checks to see if there are any corners along the path
                if(corners.Length > 0)
                {
                    int lastIndex = corners.Length - 1;
                    // Is the node the NPC at the last node on the list
                    if(currentNode >= corners.Length)
                    {
                        // Tells the functions that this node is the last node in the path
                        currentNode = lastIndex;
                    }
                    // Grabs the current corner's positon
                    Vector3 currentPos = corners[currentNode];
                    // Checks to see if the NPC is at the target
                    isAtTarget = currentNode == lastIndex;
                    // Grabs the distance to the curretn position
                    float distance = Vector3.Distance(transform.position, currentPos);
                    // Checks if the node radius is within the distance
                    if(distance <= nodeRadius)
                    {
                        //Sets the next node to the current node
                        currentNode++;
                    }
                    // If the agent is at the target
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    isAtTarget = distanceToTarget <= targetRadius;
                    // Seeks thw current node's position
                    force = Seek(currentPos);
                }
            }
        }
        return force;
    }
    #endregion
}
