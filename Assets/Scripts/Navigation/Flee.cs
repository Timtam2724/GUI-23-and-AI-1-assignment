using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Flee script follows the Steering script
public class Flee : Steering {

    #region Flee Variables
    public Transform target;
    public float stoppingDistance = 1f;
    #endregion
    #region GetForce
    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;
        if(target == null)
        {
            return force;
        }

        var desiredForce = transform.position - target.position;
        if(desiredForce.magnitude > stoppingDistance)
        {
            desiredForce = desiredForce.normalized * weighting;
            force = desiredForce - owner.velocity;
        }
        // Returns the force to GetForce
        return force;
    }
    #endregion
}
