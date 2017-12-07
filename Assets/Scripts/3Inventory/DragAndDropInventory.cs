using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropInventory : MonoBehaviour {

    #region Drag and drop variables
    [Header("Inventory Variables")]
    // To toggle the inventory on and off
    public bool showInv;
    public List<Item> inventory = new List<Item>();
    public int slotX, slotY;
    private Rect inventorySize;
    [Header("Dragging")]
    public bool dragging;
    public Item draggedItem;
    //The place where the object was from last time
    public int draggedFrom;
    public GameObject droppedItem;
    [Header("Tool Tip")]
    public int toolTipItem;
    public bool showToolTip;
    private Rect toolTipRect;
    private float scrW;
    private float scrH;
    [Header("References")]
    public Movement playerMove;
    public MouseLook mainCam, playerCam;
    #endregion

    #region Clamp
    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }
    #endregion
    #region AddItem
    public void AddItem(int iD)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].Name == null)
            {
                inventory[i] = ItemGen.CreateItem(iD);
                Debug.Log("Added Item: " + inventory[i].Name);
                return;
            }
        }
    }
    #endregion
    #region Dropping an Item
    public void DropItem(int iD)
    {
        droppedItem = Resources.Load("Prefabs/" + ItemGen.CreateItem(iD).Mesh) as GameObject;
        Instantiate(droppedItem, transform.position + transform.forward * 3, Quaternion.identity);
        return;
    }
    #endregion
    #region Drawing the item
    // This displays th icon of the object in the inventory
    void DrawItem(int windowID)
    {
        if(draggedItem.Icon != null)
        {
            GUI.DrawTexture(new Rect(0, 0, scrW * 0.5f, scrH * 0.5f), draggedItem.Icon);
        }
    }
    #endregion
    #region Tool Tip
    #region Tool Tip Content
    private string ToolTipText(int iD)
    {
        string toolTipText =
        "Name: " + inventory[iD].Name +
        "\nDescription: " + inventory[iD].Description +
        "\nType: " + inventory[iD].Type +
        "\nID: " + inventory[iD].ID;
        return toolTipText;
    }
    #endregion
    #region Tool Tip Window
    void DrawToolTip(int windowID)
    {
        GUI.Box(new Rect(0, 0, scrW * 2, scrH * 3), ToolTipText(toolTipItem));
    }
    #endregion
    #endregion
    #region Drag Inventory
    void InventoryDrag(int windowID)
    {
        GUI.Box(new Rect(0, 0.25f * scrH, 6 * scrW, 0.5f * scrH), "Banner");
        GUI.Box(new Rect(0, 4.25f * scrH, 6 * scrW, 0.5f * scrH), "Gold n EXP");
        showToolTip = false;
        #region Nested For Loop
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < slotY; y++)
        {
            for (int x = 0; x < slotX; x++)
            {
                Rect slotLocation = new Rect(scrW * 0.125f + x * (scrW * 0.75f), scrH * 0.75f + y * (scrH * 0.65f), 0.75f * scrW, 0.65f * scrH);
                GUI.Box(slotLocation, "");
                #region Pickup Item
                if(e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !dragging && inventory[i].Name != null && !Input.GetKey(KeyCode.LeftShift))
                {
                    draggedItem = inventory[i]; // Pick up item
                    inventory[i] = new Item(); // Restores the previous slot to a blank slot
                    dragging = true; // We are draging an object
                    draggedFrom = i; // We know where the object was last placed
                    Debug.Log("Moving: " + draggedItem.Name);
                }
                #endregion
                // Swapping items in the inventory
                #region Swap Item
                if(e.button == 0 && e.type == EventType.MouseUp & slotLocation.Contains(e.mousePosition) && dragging && inventory[i].Name != null)
                {
                    Debug.Log("Swapping: " + draggedItem.Name + " :Into: " + i);
                    inventory[draggedFrom] = draggedItem; // Our pick up slot has the other object
                    inventory[i] = draggedItem; // Places the item we were draaging into the new empty slot
                    draggedItem = new Item(); 
                    dragging = false; // Tells the game that after the object has been moved the player's hand is now back to empty
                }
                #endregion
                #region Place Item
                if(e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition)&& dragging && inventory[i].Name == null)
                {
                    Debug.Log("Place: " + draggedItem.Name + " :Item: " + i);

                    inventory[i] = draggedItem;
                    draggedItem = new Item();
                    dragging = false;
                }
                #endregion
                #region Return Item
                if (e.button == 0 && e.type == EventType.MouseUp && i == ((slotX*slotY)-1) && dragging)
                {
                    inventory[draggedFrom] = draggedItem;
                    draggedItem = new Item();
                    dragging = false;
                }
                #endregion
                // This function displays the icon for the following item in the inventory
                #region Draw Item Icon
                if (inventory[i].Name != null)
                {
                    GUI.DrawTexture(slotLocation, inventory[i].Icon);
                    if(slotLocation.Contains(e.mousePosition) && !dragging && showInv)
                    {
                        toolTipItem = i;
                        showToolTip = true;
                    }
                }
                #endregion
                i++;
            }
        }
        #endregion
        #region Drag Window
        GUI.DragWindow(new Rect(0 * scrW, 0 * scrH, 6 * scrW, 0.5f * scrH)); // Top Drag
        GUI.DragWindow(new Rect(0 * scrW, 0.5f * scrH, 0.25f * scrW, 3.5f * scrH)); // Left Drag
        GUI.DragWindow(new Rect(5.5f * scrW, 0.5f * scrH, 0.25f * scrW, 3.5f * scrH)); // Right Drag
        GUI.DragWindow(new Rect(0 * scrW, 4 * scrH, 0.25f * scrW, 3.5f * scrH)); // Bottom Drag
        #endregion
    }
    #endregion
    // Use this for initialization
    void Start () {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
        mainCam = Camera.main.GetComponent<MouseLook>();
        playerCam = GetComponent<MouseLook>();
        playerMove = GetComponent<Movement>();
        inventorySize = new Rect(scrW, scrH, 6 * scrW, 4.5f * scrH);
        for (int i = 0; i < (slotX * slotY); i++)
        {
            inventory.Add(new Item());
        }
        // Adds the following items to the inventory
        AddItem(0);
        AddItem(0);
        AddItem(1);
        AddItem(102);
        AddItem(206);
        AddItem(100);
        AddItem(800);
	}	

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInv();
        }
	}

    #region Toggle Inventory
    public bool ToggleInv()
    {
        if (showInv)
        {            
            showInv = false; // removes the inventory
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked; // The cursor is locked while we are not in the inventory
            Cursor.visible = false; 
            mainCam.enabled = true; // The player can look around as normal while outside the inventory
            playerCam.enabled = true;
            playerMove.enabled = true; // The player can also move at the point
            return (false);
        }
        else
        {
            showInv = true; // If the inventory is activated
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; // The cursor is visable to allow the player to grab and drop items in the inventory
            mainCam.enabled = false; // While using the inventory all other player abilities are disabled for now
            playerCam.enabled = false;
            playerMove.enabled = false;
            return (true);
        }
    }
    #endregion
    #region OnGUI
    void OnGUI()
    {
        Event e = Event.current;
        // Displays the title inventory over the inventory
        #region Draw Inventory if activated
        if (showInv)
        {
            inventorySize = ClampToScreen(GUI.Window(1, inventorySize, InventoryDrag, "My Inventory"));
        }
        #endregion
        #region Draw the tool Tip
        if(showToolTip && showInv)
        {
            toolTipRect = new Rect(e.mousePosition.x + 0.01f, e.mousePosition.y + 0.01f, scrW * 2, scrH * 3);
            GUI.Window(15, toolTipRect, DrawToolTip, "");
        }
        #endregion
        #region Drop Item is not showInv and mouse is up
        if(e.button == 0 && e.type == EventType.MouseUp && dragging)
        {
            DropItem(draggedItem.ID);
            Debug.Log("Dropped: " + draggedItem.Name);
            draggedItem = new Item();
            dragging = false;
        }
        #endregion
        #region Incase inventory closes
        if(e.button == 0 && e.type == EventType.MouseUp && dragging && !showInv)
        {
            DropItem(draggedItem.ID);
            Debug.Log("Dropped: " + draggedItem.Name);
            draggedItem = new Item();
            dragging = false;
        }
        #endregion
        #region DrawItem on Mouse
        if (dragging)
        {
            if(draggedItem != null)
            {
                Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y + 0.125f, scrW * 0.5f, scrH * 0.5f);
                GUI.Window(2, mouseLocation, DrawItem, "");
            }
        }
        #endregion
    }
    #endregion
}
