using System.Collections;
using UnityEngine;

[AddComponentMenu("Character Set Up/Interact")]
public class Pickup : MonoBehaviour {

    #region Player Camera connection
    // Refers to these gameobjects as the player and the main Camera
    public GameObject player;
    public GameObject mainCam;
    #endregion

    // Use this for initialization
    void Start ()
    {
        // Identifies the player by the tag
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        #region When E is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Creates a ray
            Ray interact;
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            // Gives info on what the player has hit
            RaycastHit hitinfo;
            if(Physics.Raycast(interact,out hitinfo, 10))
            {
                #region Hitting a NPC
                //Note saying that we hit the NPC
                if (hitinfo.collider.CompareTag("NPC"))
                {
                    Debug.Log("Hit a NPC");
                    Dialogue dlg = hitinfo.transform.GetComponent<Dialogue>();
                    if(dlg != null)
                    {
                        dlg.showDlg = true;
                        player.GetComponent<MouseLook>().enabled = false;
                        player.GetComponent<Movement>().enabled = false;
                        mainCam.GetComponent<MouseLook>().enabled = false;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
                #endregion
                // The procedure if the player hits an item
                #region Hitting an item
                if (hitinfo.collider.CompareTag("item"))
                {
                    Debug.Log("Hit an Item");                                       
                }
                #endregion
                #region Hitting an Enemy
                if (hitinfo.collider.CompareTag("Enemy"))
                {
                    Debug.Log("Dealt 10 damage!");
                    // Grabs the dialogue in the NPC or Enemy
                    Dialogue dlg = hitinfo.transform.GetComponent<Dialogue>();
                    if (dlg != null)
                    {                        
                        dlg.showDlg = true;
                        player.GetComponent<MouseLook>().enabled = false;
                        player.GetComponent<Movement>().enabled = false;
                        mainCam.GetComponent<MouseLook>().enabled = false;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
                #endregion
            }
        }
        #endregion
    }
}
