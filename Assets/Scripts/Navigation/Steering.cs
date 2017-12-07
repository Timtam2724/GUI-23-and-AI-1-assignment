using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIagent))]
public class Steering : MonoBehaviour {

    #region Variables
    public float weighting = 7.5f;
    public Vector3 force;
    public AIagent owner;
    #endregion
    #region Awake
    protected virtual void Awake()
    {
        // Grabs the AIAgent
        owner = GetComponent<AIagent>();
    }
    #endregion
    #region GetForce
    public virtual Vector3 GetForce()
    { 
        return Vector3.zero;
    }
    #endregion
}
