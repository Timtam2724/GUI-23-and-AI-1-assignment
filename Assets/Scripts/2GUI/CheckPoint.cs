using System.Collections;
using UnityEngine;

[AddComponentMenu("Character Set Up/Checkpoint")]
public class CheckPoint : MonoBehaviour {

    #region Char Variables
    public GameObject curCheckPoint;
    public CharacterHandler charH;
    #endregion
    #region Start
    // Use this for initialization
    void Start () {
        charH = this.GetComponent<CharacterHandler>();
        if (PlayerPrefs.HasKey("SpawnPoint"))
        {
            curCheckPoint = GameObject.Find(PlayerPrefs.GetString("SpawnPoint"));
            this.transform.position = curCheckPoint.transform.position;
        }
	}
    #endregion
    #region Update
    // Update is called once per frame
    void Update ()
    {
        // THe function that respawns the player at the nearest checkpoint after death
        #region Respawner
        if(charH.curHealth == 0)
        {
            this.transform.position = curCheckPoint.transform.position;
            charH.curHealth = charH.maxHealth;
            // Makes the player alive again
            charH.alive = true;
            // Restores the character controller
            CharacterController controller = this.GetComponent<CharacterController>();
            controller.enabled = true;
        }
        #endregion       
    }
    #endregion
    #region Setting the latest checkpoint as default
    void OnTriggerEnter(Collider other)
    {
        // Activates if player collides with an object tagged Checkpoint
        if (other.CompareTag("CheckPoint"))
        {
            curCheckPoint = other.gameObject;
            // Assignsthe object as the new checkpoint
            PlayerPrefs.SetString("SpawnPoint", curCheckPoint.name);
        }
    }
    #endregion
}
