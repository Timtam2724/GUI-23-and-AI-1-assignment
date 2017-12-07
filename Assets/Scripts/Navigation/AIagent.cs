using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIagent : MonoBehaviour {

    #region Variables
    public Vector3 force;
    public Vector3 velocity;
    public float maxVelocity = 100f;
    public float maxDistance = 10f;
    public bool freezeRotation = false;
    public bool updatePosition = true;
    public bool updateRotation = true;

    private NavMeshAgent nav;
    private List<Steering> behaviours;
    #endregion
    #region
    // Starts when the game begins
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        behaviours = new List<Steering>(GetComponents<Steering>());
    }
    #endregion
    #region Update
    // Update is called once per frame
    void Update () {
        // It constantly activates the two functions
        ComputeForce();
        ApplyVelocity();
        // Sets the nav position and rotation to be what the bool is set to at the time
   //     nav.updatePosition = updatePosition;
   //     nav.updateRotation = updateRotation;
	}
    #endregion
    #region ComputeForce
    void ComputeForce()
    {
        force = Vector3.zero;
        for (int i = 0; i < behaviours.Count; i++)
        {
            Steering behaviour = behaviours[i];
            if(behaviour.isActiveAndEnabled == false)
            {
                continue;
            }
            force = force + behaviour.GetForce()*behaviour.weighting;
            if(force.magnitude > maxVelocity)
            {
                force = force.normalized * maxVelocity;
                break;
            }
        }
    }
    #endregion
    #region Apply Velocity
    void ApplyVelocity()
    {
        velocity = velocity + force * Time.deltaTime;
        if(velocity.magnitude > maxVelocity)
        {
            velocity = velocity.normalized * maxVelocity;
        }
        if(velocity.magnitude > 0)
        {
            transform.position = transform.position + velocity * Time.deltaTime;
        }
    }
    #endregion
}
