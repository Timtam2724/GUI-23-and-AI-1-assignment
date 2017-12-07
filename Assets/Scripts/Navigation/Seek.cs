using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : Steering {

    // Sets the target
    public Transform target;
    public float stoppingDistance = 1f;

    // The script overrides the original GetForce
    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;
        // If no target is set
        if(target == null)
        {
            return force;
        }
        var desiredForce = target.position - transform.position;
        if(desiredForce.magnitude > stoppingDistance)
        {
            desiredForce = desiredForce.normalized * weighting;
            if(desiredForce.magnitude > stoppingDistance)
            {
                desiredForce = desiredForce.normalized * weighting;
                force = desiredForce - owner.velocity;
            }
        }
        // Returns the force to GetForce
        return force;
    }

}
