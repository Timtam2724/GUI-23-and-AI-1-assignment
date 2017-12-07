using System.Collections;
using UnityEngine;

[AddComponentMenu("Character Set Up/CharacterController Handler")]
public class CharacterHandler : MonoBehaviour {

    #region Character Variable and Minimap
    [Header("Character Variables")]
    public bool alive;
    public CharacterController controller;
    public float maxHealth, curHealth;
    public float maxMana, curMana;
    public float maxStamina, curStamina;
    public int level;
    public int maxExp, curExp;
    [Header("Minimap Connection")]
    public RenderTexture miniMap;
    #endregion
    // Use this for initialization
    void Start () {
        //The player's maxmimum health
        maxHealth = 100f;
        curHealth = maxHealth;
        maxMana = 80f;
        curMana = maxMana;
        maxStamina = 200f;
        curStamina = maxStamina;
        // Makes sure the player is alive at the start of the game
        alive = true;
        maxExp = 60;
        controller = this.GetComponent<CharacterController>();        
    }
    #region Update
    // Update is called once per frame
    void Update ()
    {
        // What to do if the amount of experience point goes over the max experience
		if(curExp >= maxExp)
        {
            curExp -= maxExp;
            // The level icreases
            level++;
            // The health of the player increases
            maxExp += 50;
        }
        // The player's health is increased when constitution is increased        
        maxHealth = 100 + (stats.constitution * 100) + (level * 50);
        // As a warrior the rate of mana is decreased
        maxMana = 80 + (stats.wisdom * 50) + (level * 30);
        // Warriors have high stamina
        maxStamina = 200 + (stats.strength * 150) + (level * 70);
    }
    #endregion
    #region LateUpdate
    void LateUpdate()
    {
        // What to do if our health is greater than our maximum health
        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        // if our current health is less than zero or if the player's alive status is set to false
        if(curHealth < 0 || !alive)
        {
            // Sets the player's health to zero
            curHealth = 0;
        }
        // What happens if the player is still alive despite the current health is zero
        if (alive)
        {
            if(curHealth < 0)
            {
                // The player should be dead so alive is false
                alive = false;
                // While dead the player can't move
                controller.enabled = false;
            }
        }      
    }
    #endregion
    #region Player Health and EXP
    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        // GUI for the Health bar
        GUI.Box(new Rect(6 * scrW, 0.25f * scrH, 4 * scrW, 0.5f * scrH), "Health");
        GUI.Box(new Rect(6 * scrW, 0.25f * scrH, curHealth * (4 * scrW) / maxHealth, 0.5f * scrH), "");
        // GUI for the Mana (Warriors don't have much mana)
        GUI.Box(new Rect(6 * scrW, 0.75f *scrH, 2 * scrW, 0.25f * scrH), "Mana");
        GUI.Box(new Rect(6 * scrW, 0.75f *scrH, curMana * (2 * scrW) / maxMana, 0.25f * scrH), "");
        // GUI for the Stamina
        GUI.Box(new Rect(8 * scrW, 0.75f * scrH, 4 * scrW, 0.25f * scrH), "Stamina");
        GUI.Box(new Rect(8 * scrW, 0.75f * scrH, curStamina * (4 * scrW) / maxStamina, 0.25f * scrH), "");
        // GUI for the Experience
        GUI.Box(new Rect(6 * scrW, scrH, 4 * scrW, 0.25f * scrH), "");
        GUI.Box(new Rect(6 * scrW, scrH, curExp * (4 * scrW) / maxExp, 0.25f * scrH), "");
        //GUI minimap texture
        GUI.DrawTexture(new Rect(13 * scrW, 0.25f * scrH, 2.925f * scrW, 2.5f * scrH), miniMap);
    }
    #endregion
}
