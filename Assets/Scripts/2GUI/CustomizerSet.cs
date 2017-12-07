using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizerSet : MonoBehaviour {

    #region Character Variables
    // Grabs all the possible skins put them in a list
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();

    public int skinIndex;
    public int hairIndex, mouthIndex, eyesIndex, clothesIndex, armourIndex;
    public Renderer character;
    public int skinMax;
    public int hairMax, mouthMax, eyesMax, clothesMax, armourMax;

    public string charName = "Warrior";
    #endregion
    #region Start
    // Use this for initialization
    void Start () {
        #region Pull textures from Resources file
        for (int i = 0; i < skinMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Skin_" + i.ToString()) as Texture2D;
            skin.Add(temp);
        }
        for (int i = 0; i < hairMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Hair_" + i.ToString()) as Texture2D;
            hair.Add(temp);
        }
        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Mouth_" + i.ToString()) as Texture2D;
            mouth.Add(temp);
        }
        for (int i = 0; i < eyesMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Eyes_" + i.ToString()) as Texture2D;
            eyes.Add(temp);
        }
        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Clothes_" + i.ToString()) as Texture2D;
            clothes.Add(temp);
        }
        for (int i = 0; i < armourMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Armour_" + i.ToString()) as Texture2D;
            armour.Add(temp);
        }
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        #endregion
        // Remember to write this next bit in after writing SetTexture
        // Sets it to default textures
        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Clothes", 0);
        SetTexture("Armour", 0);
    }
    #endregion
    // Sets the texture on the character
    #region SetTexture
    void SetTexture(string type, int dir)
    {
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        switch (type)
        {
            // SKin is matIndex 1
            case "Skin":
                index = skinIndex;
                max = skinMax;
                textures = skin.ToArray();
                matIndex = 1;
                break;
            // Hair is matIndex 2
            case "Hair":
                index = hairIndex;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 2;
                break;
            // Mouth is matIndex 3
            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 3;
                break;
            // Eyes is matIndex 4
            case "Eyes":
                index = eyesIndex;
                max = eyesMax;
                textures = eyes.ToArray();
                matIndex = 4;
                break;
            // Clothes is matIndex 5
            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 5;
                break;
            // Armour is matIndex 6
            case "Armour":
                index = armourIndex;
                max = armourMax;
                textures = armour.ToArray();
                matIndex = 6;
                break;
        }
        index += dir;
        // If we go over the max numbe of skins for that object
        if(index < 0)
        {
            index = max - 1;
        }
        if(index > max - 1)
        {
            index = 0;
        }
        Material[] mat = character.materials;
        mat[matIndex].mainTexture = textures[index];
        character.materials = mat;
        // Remeber to make one for each material
        switch (type)
        {
            case "Skin":
                skinIndex = index;
                break;
            case "Hair":
                hairIndex = index;
                break;
            case "Mouth":
                mouthIndex = index;
                break;
            case "Eyes":
                eyesIndex = index;
                break;
            case "Clothes":
                skinIndex = index;
                break;
            case "Armour":
                skinIndex = index;
                break;
        }
    }
    #endregion
    // This will save our character textures
    #region Save
    void Save()
    {
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);
        //As the player name assigned as a word not number it is string
        PlayerPrefs.SetString("CharacterName", charName);
    }
    #endregion

    // A special function for GUI
    #region OnGUI
    void OnGUI()
    {
        // Floats that assign the values for scrW and scrH
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        int i = 0;
        // Skin Buttons
        if(GUI.Button(new Rect(0.25f*scrW, scrH+i*(0.5f*scrH), 0.5f*scrW, 0.5f*scrH), "<"))
        {
            // Grabs the skin material before the current skin material
            SetTexture("Skin", -1);
        }
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "Skin");
        if(GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetTexture("Skin", 1);
        }
        i++;
        // Hair Buttons
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // Grabs the skin material before the current skin material
            SetTexture("Hair", -1);
        }
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "Hair");
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetTexture("Hair", 1);
        }
        i++;
        // Mouth Buttons
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // Grabs the skin material before the current skin material
            SetTexture("Mouth", -1);
        }
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "Mouth");
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetTexture("Mouth", 1);
        }
        i++;
        // Eyes Buttons
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // Grabs the skin material before the current skin material
            SetTexture("Eyes", -1);
        }
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "Eyes");
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetTexture("Eyes", 1);
        }
        i++;
        // Clothes Buttons
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // Grabs the skin material before the current skin material
            SetTexture("Clothes", -1);
        }
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "Clothes");
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetTexture("Clothes", 1);
        }
        i++;
        // Armour Buttons
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            // Grabs the skin material before the current skin material
            SetTexture("Armour", -1);
        }
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "Skin");
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetTexture("Armour", 1);
        }
        i++;

        // The Random Texture Button
        if(GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Reset"))
        {
            SetTexture("Skin", skinIndex = 0);
            SetTexture("Hair", hairIndex = 0);
            SetTexture("Mouth", mouthIndex = 0);
            SetTexture("Eyes", eyesIndex = 0);
            SetTexture("Clothes", clothesIndex = 0);
            SetTexture("Armour", armourIndex = 0);
        }
        i++;
        // Textbox for creating character's name
        charName = GUI.TextField(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), charName, 12);
        // Remember to add the i++ to make the next GUI element move down
        i++;
        // The save and play button
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Save & Play"))
        {
            // Activates the save script and moves to the next scene
            Save();
            SceneManager.LoadScene("The Game");
        }
    }
    #endregion
}
