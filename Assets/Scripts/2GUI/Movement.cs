using System.Collections;
using UnityEngine;

[AddComponentMenu("Character SetUp/Character Movement")]
// Requires a Character Controller to function
// If there is no Character Controller then the script adds one to the object
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour {

    #region Movement Variables
    [Header("Character mover")]
    public Vector3 moveDir = Vector3.zero;
    private CharacterController charC;
    // Variable of the character that can be altered in the unity inspector
    public float jumpSpeed = 8.0f;
    public float speed = 6.0f;
    public float gravity = 20.0f;
    #endregion
    #region Start
    // Use this for initialization
    void Start () {
        // charC is refered to as the Character Controller
        charC = this.GetComponent<CharacterController>();
	}
    #endregion
    #region
    // Update is called once per frame
    void Update () {
        // If the character is on the ground
        if (charC.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
            // While grounded the player can jump
            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }
        }
        moveDir.y -= gravity * Time.deltaTime;
        charC.Move(moveDir * Time.deltaTime);
        speed = stats.dexterity + 6;
	}
    #endregion
}
