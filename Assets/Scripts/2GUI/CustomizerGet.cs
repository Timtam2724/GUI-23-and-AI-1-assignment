using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizerGet : MonoBehaviour {

    public Renderer character;

	// Use this for initialization
	void Start () {
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        LoadTexture();
	}
    #region Loads the Texture
    public void LoadTexture()
    {
        // If the player has set a name for the character then the game begins
        if (!PlayerPrefs.HasKey("CharacterName"))
        {
            Application.LoadLevel(1);
        }
        SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
        SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
        SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
        SetTexture("Eyes", PlayerPrefs.GetInt("EyesIndex"));
        SetTexture("Clothes", PlayerPrefs.GetInt("ClothesIndex"));
        SetTexture("Armour", PlayerPrefs.GetInt("ArmourIndex"));
        gameObject.name = PlayerPrefs.GetString("CharacterName");
    }
    #endregion
    #region SetTexture
    // Assigns a different matIndex for each skin on the character
    public void SetTexture(string type, int dir)
    {
        Texture2D tex = null;
        int matIndex = 0;
        switch (type)
        {
            case "Skin":
                tex = Resources.Load("Character/Skin_" + dir.ToString()) as Texture2D;
                matIndex = 1;
                break;
            case "Hair":
                tex = Resources.Load("Character/Hair_" + dir.ToString()) as Texture2D;
                matIndex = 2;
                break;
            case "Mouth":
                tex = Resources.Load("Character/Mouth_" + dir.ToString()) as Texture2D;
                matIndex = 3;
                break;
            case "Eyes":
                tex = Resources.Load("Character/Eyes_" + dir.ToString()) as Texture2D;
                matIndex = 4;
                break;
            case "Clothes":
                tex = Resources.Load("Character/Clothes_" + dir.ToString()) as Texture2D;
                matIndex = 5;
                break;
            case "Armour":
                tex = Resources.Load("Character/Armour_" + dir.ToString()) as Texture2D;
                matIndex = 6;
                break;
        }
        Material[] mats = character.materials;
        mats[matIndex].mainTexture = tex;
        character.materials = mats;
    }
    #endregion
}
