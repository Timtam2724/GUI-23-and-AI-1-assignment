using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Inventory Variables
    public List<Item> inv = new List<Item>();
    public bool showInv;
    public Item selectedItem;
    public int money;
    public MouseLook mainCam, playerCam;
    public Movement playerMove;
    public Vector2 scrollPos;
    public GameObject wHand, curWeapon;
    public GameObject hHandler, curHelm;
    #endregion
    // Use this for initialization
    void Start()
    {
        // The player starts with these items
        #region Starting items
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(1));
        inv.Add(ItemGen.CreateItem(100));
        inv.Add(ItemGen.CreateItem(201));
        inv.Add(ItemGen.CreateItem(205));
        inv.Add(ItemGen.CreateItem(207));
        inv.Add(ItemGen.CreateItem(208));
        inv.Add(ItemGen.CreateItem(700));
        #endregion
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
        playerCam = this.GetComponent<MouseLook>();
        playerMove = this.GetComponent<Movement>();
        //If the player takes out a weapon or helmet, the game knows where to place it
        wHand = GameObject.FindGameObjectWithTag("Weapon Handler");
        hHandler = GameObject.FindGameObjectWithTag("HeadHandler");
    }
    #region Turning inventory on and off
    // Update is called once per frame
    void Update()
    {
        // Turns the inventory menu on and off
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInv();
        }
    }
    // When active the inventory appears and the player stops
    public bool ToggleInv()
    {
        if (showInv)
        {
            showInv = false;
            Time.timeScale = 1;
            mainCam.enabled = true;
            playerCam.enabled = true;
            playerMove.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            return (false);
        }
        // As a bool it needs to has a true and false function to switch between
        else
        {
            showInv = true;
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
    void OnGUI()
    {
        // Requires the inventory to be true
        if (showInv)
        {
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;
            // Background for inventory labled Inventory
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Bag");
            // If less than or equal to x items no scroll view
            if (inv.Count <= 35)
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scrW, 0.25f * scrH + i * (0.25f * scrH), 3 * scrW, 0.25f * scrH), inv[i].Name))
                    {
                        selectedItem = inv[i];
                        Debug.Log(selectedItem.Name);
                    }
                }
            }
            else
            // If more then we can scroll and add the same space according to the amount of stuff we have
            {
                scrollPos = GUI.BeginScrollView(new Rect(0, 0, 6 * scrW, 9 * scrH), scrollPos, new Rect(0, 0, 0, 9 * scrH + ((inv.Count - 35) * 0.25f * scrH)), false, true);
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scrW, 0.25f * scrH + i * (0.25f * scrH), 3 * scrW, 0.25f * scrH), inv[i].Name))
                    {
                        selectedItem = inv[i];
                        Debug.Log(selectedItem.Name);
                    }
                }
                GUI.EndScrollView();
            }
            // If we are holding an item and not nothing
            if (selectedItem != null)
            {
                if (selectedItem.Type == ItemType.Food)
                {
                    GUI.Box(new Rect(8 * scrW, 5 * scrH, 8 * scrW, 3 * scrH), selectedItem.Name + "\n" + selectedItem.Description + "\n" + selectedItem.Value);
                    GUI.DrawTexture(new Rect(11 * scrW, 1.5f * scrH, 2 * scrW, 2 * scrH), selectedItem.Icon);
                    if (GUI.Button(new Rect(15 * scrW, 8.75f * scrH, scrW, 0.25f * scrH), "Eat"))
                    {
                        // Puts a message after eating
                        Debug.Log("Tasty " + selectedItem.Name);
                        // Tells the game to remove the object after it is used
                        inv.Remove(selectedItem);
                        // Tells the game that the player's hand is back to being empty
                        selectedItem = null;
                    }
                }
                if (selectedItem.Type == ItemType.Weapon)
                {
                    GUI.Box(new Rect(8 * scrW, 5 * scrH, 8 * scrW, 3 * scrH), selectedItem.Name + "\n" + selectedItem.Description + "\n" + selectedItem.Value);
                    GUI.DrawTexture(new Rect(11 * scrW, 1.5f * scrH, 2 * scrW, 2 * scrH), selectedItem.Icon);
                    if (GUI.Button(new Rect(15 * scrW, 8.75f * scrH, scrW, 0.25f * scrH), "Equip"))
                    {
                        Debug.Log("Attack with " + selectedItem.Name);
                        if (curWeapon != null)
                        {
                            Destroy(curWeapon);
                        }
                        curWeapon = Instantiate(Resources.Load("Prefabs/" + selectedItem.Mesh) as GameObject, wHand.transform.position, wHand.transform.rotation, wHand.transform);
                        selectedItem = null;
                    }
                }
                if (selectedItem.Type == ItemType.Apparel)
                {
                    GUI.Box(new Rect(8 * scrW, 5 * scrH, 8 * scrW, 3 * scrH), selectedItem.Name + "\n" + selectedItem.Description + "\n" + selectedItem.Value);
                    GUI.DrawTexture(new Rect(11 * scrW, 1.5f * scrH, 2 * scrW, 2 * scrH), selectedItem.Icon);
                    if (GUI.Button(new Rect(15 * scrW, 8.75f * scrH, scrW, 0.25f * scrH), "Wear"))
                    {
                        Debug.Log(selectedItem.Name);
                        if (curHelm != null)
                        {
                            Destroy(curHelm);
                        }
                        curHelm = Instantiate(Resources.Load("Prefabs/" + selectedItem.Mesh) as GameObject, hHandler.transform.position, hHandler.transform.rotation, hHandler.transform);
                        selectedItem = null;
                    }
                }
                if (selectedItem.Type == ItemType.Crafting)
                {

                }
                if (selectedItem.Type == ItemType.Ingredients)
                {

                }
                if (selectedItem.Type == ItemType.Potions)
                {
                    
                }
                if (selectedItem.Type == ItemType.Scrolls)
                {

                }
                }
            }
        }
    }
