using System.Collections;
using UnityEngine;
using GGL;

public class Wander : Steering {

    #region Wander Variables
    public float offset = 0f;
    public float radius = 1.0f;
    public float jitter = 0.2f;
   
    private Vector3 targetDir;
    private Vector3 randomDir;
    #endregion

    #region Get Force
    public override Vector3 GetForce()
    {
        // The force starts at 0
        Vector3 force = Vector3.zero;

        // This part randomizes the values of randX and randZ
        float randX = Random.Range(0, 0x7fff) - (0x7fff * 0.5f);
        float randZ = Random.Range(0, 0x7fff) - (0x7fff * 0.5f);

        #region Calculate Random Direction
        // Creates the random direction
        randomDir = new Vector3(randX, 0, randZ);
        randomDir = randomDir.normalized;
        // Applifies the randomness by the jitter
        randomDir *= jitter;
        #endregion
        
        #region Calculate the direction of the target
        // The random Direction is either added or equal to the target direction
        targetDir += randomDir;
        // Normalize the target dir
        targetDir = targetDir.normalized;
        targetDir *= radius;
        #endregion
        

        // Calulates the seek position using the target direction
        Vector3 seekPos = transform.position + targetDir;
        seekPos += transform.forward.normalized * offset;

        Vector3 forwardPos = transform.position + transform.forward.normalized * offset;
        #region Circle Gizmos
        Circle c = GizmosGL.AddCircle(forwardPos, radius, Quaternion.LookRotation(Vector3.down));
        c.color = new Color(1, 0, 0, 0.5f);
        c = GizmosGL.AddCircle(seekPos, radius * 0.6f, Quaternion.LookRotation(Vector3.down));
        c.color = new Color(0, 0, 1, 0.5f);
        #endregion

        #region Wander
        // Calculates the direction
        Vector3 direction = seekPos - transform.position;
        // Checks if the direction is not zero
        if(direction.magnitude > 0)
        {
            // Calculaters the force
            Vector3 desiredForce = direction.normalized * weighting;
            force = desiredForce - owner.velocity;
        }
        #endregion
        // Remember to return the force at the end
        return force;
    }
    #endregion
}
