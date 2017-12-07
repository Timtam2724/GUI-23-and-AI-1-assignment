using System.Collections;
using UnityEngine;

[AddComponentMenu("Character Set Up/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    #region Mouselook variables
    public RotationAxis axis = RotationAxis.MouseX;
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;
    public float minimumY = -60f;
    public float maximumY = 60f;
    float rotationY = 0;
    #endregion
    #region Start
    // Use this for initialization
    void Start()
    {
        if (this.GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
    #endregion
    #region Update
    // Update is called once per frame
    void Update()
    {
        if (axis == RotationAxis.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        // Mouse X
        else if (axis == RotationAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        //Mouse Y
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }
    #endregion
}
//public enums of the rotation axis
#region RotationAxis
public enum RotationAxis
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
#endregion

