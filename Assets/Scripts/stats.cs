using System.Collections;
using UnityEngine;

public class stats : MonoBehaviour
{
    #region Stat Variables
    // How many points the player can alocate to certain skills
    public int points = 10;
    public static int dexterity, strength, constitution, wisdom, intellegence, charisma;
    public bool showStats;
    public MouseLook mainCam, playerCam;
    public Movement playerMove;
    public Rect windowRect = new Rect(10, 200, 130, 100);
    #endregion
    #region ShowStats Controller disabler
    // Displays and hides the stats
    public bool ToggleStats()
    {
        if (showStats)
        {
            showStats = false;
            Time.timeScale = 1;
            mainCam.enabled = true;
            playerCam.enabled = true;
            playerMove.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            return (false);
        }
        // Bools are either true or false
        else
        {
            showStats = true;
            Time.timeScale = 0;
            mainCam.enabled = false;
            playerCam.enabled = false;
            playerMove.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return (true);
        }
    }
    #endregion
    #region Update
    // Button to turn on and off the stats
    void Update () {
        // Press Q to toggle stats
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleStats();
        }
	}
    #endregion
    #region Options Window
    void DoMyWindow(int windowID)
    {
        // Resets all the skill points
        if (GUI.Button(new Rect(13, 20, 100, 30), "Reset"))
        {
            dexterity = 0;
            strength = 0;
            intellegence = 0;
            charisma = 0;
            wisdom = 0;
            constitution = 0;
            points = 10;
        }
        // Exits the game
        if (GUI.Button(new Rect(13, 60, 100, 30), "Quit"))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
    #endregion
    #region GUI
    // Press Q to toggle stat window
    void OnGUI()
    {        
        // If the showStats is active then the GUI is active
        if (showStats)
        {
            windowRect = GUI.Window(0, windowRect, DoMyWindow, "Options");
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;
            int i = 0;
            GUI.Box(new Rect(0.65f * scrW, scrH + i * (0.5f * scrH), 1.2f * scrW, 0.5f * scrH), "Points: " + points.ToString());
            i++;
            #region Dexterity
            // Only shows up if you still have at least 1 avaliable points and your dexterity is above 0
            if (points < 10 && dexterity > 0)
            {
                if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
                {
                    // Lose a point from Dexterity, but adds a point to your avaliable points
                    dexterity--;
                    points++;
                }
            }
            GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Dex: " + dexterity.ToString());
            if (points > 0)
            {
                if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
                {
                    // Adds a point to Dexterity, but loses a point from the avaliable points
                    dexterity++;
                    points--;
                }
            }
            //Move Down a space
            i++;
            #endregion
            #region Strength
            if (points < 10 && strength > 0)
            {
                if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
                {
                    strength--;
                    points++;
                }
            }
            GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Str: " + strength.ToString());
            if (points > 0)
            {
                if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
                {
                    strength++;
                    points--;
                }
            }
            i++;
            #endregion
            #region Wisdom
            if (points < 10 && wisdom > 0)
            {
                if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
                {
                    wisdom--;
                    points++;
                }
            }
            GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Wis: " + wisdom.ToString());
            if (points > 0)
            {
                if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
                {
                    wisdom++;
                    points--;
                }
            }
            i++;
            #endregion
            #region Intel
            if (points < 10 && intellegence > 0)
            {
                if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
                {
                    intellegence--;
                    scrW = scrW - 1;
                    scrH = scrH - 1;
                    points++;
                }
            }
            GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Int: " + intellegence.ToString());
            if (points > 0)
            {
                if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
                {
                    intellegence++;
                    scrW = scrW + 1;
                    scrH = scrH + 1;
                    points--;
                }
            }
            i++;
            #endregion
            #region Charisma
            if (points < 10 && charisma > 0)
            {
                if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
                {
                    charisma--;
                    Debug.Log("Ick");
                    points++;
                }
            }
            GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Cha: " + charisma.ToString());
            if (points > 0)
            {
                if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
                {
                    charisma++;
                    Debug.Log("I'm sexy and I know it!");
                    points--;
                }
            }
            i++;
            #endregion
            #region Constitution
            if (points < 10 && constitution > 0)
            {
                if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
                {
                    constitution--;
                    points++;
                }
            }
            GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Con: " + constitution.ToString());
            if (points > 0)
            {
                if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
                {
                    constitution++;

                    points--;
                }
            }
            i++;
            #endregion            
        }        
    }
    #endregion
}
