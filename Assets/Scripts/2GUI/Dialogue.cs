using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[AddComponentMenu("NPC/Dialogue")]
public class Dialogue : MonoBehaviour
{

    #region Variables
    public GUISkin customSkin;
    public int index, optionIndex;
    public bool showDlg;
    public GameObject player;
    public MouseLook mainCam;
    [Header("Dialogue Variables")]
    public string npcName;
    public string[] text;
    #endregion
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();        
    }

    #region GUI elements
    void OnGUI()
    {
        GUI.skin = customSkin;
                
        // If the Dialogue is set to true
        if (showDlg)
        {
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;
            // When the inventory is up the time in the world freezes, 0 means no time in the world
            Time.timeScale = 0;
            GUI.Box(new Rect(0, 6 * scrH, Screen.width, 3 * scrH), text[index]);
            if (!(index + 1 >= text.Length || index == optionIndex))
            {
                if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Next"))
                {
                    index++;
                }
            }
            else if (index == optionIndex)
            {
                if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Talk"))
                {
                    index++;
                }
                if (GUI.Button(new Rect(14 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Leave"))
                {
                    index = text.Length - 1;
                }
            }
            else
            {
                // After selecting the Bye option the dialogue is deactivated
                if (GUI.Button(new Rect(15 * scrW, 8.5f * scrH, scrW, 0.5f * scrH), "Bye."))
                {
                    // The time is set back to normal when set to 1
                    Time.timeScale = 1;
                    index = 0;
                    showDlg = false;
                    player.GetComponent<Movement>().enabled = true;
                    player.GetComponent<MouseLook>().enabled = true;
                    mainCam.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }
#endregion
}
