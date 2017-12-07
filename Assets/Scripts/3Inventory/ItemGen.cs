using UnityEngine;

// Public static means it can be accessed by other scripts
public static class ItemGen
{

    public static Item CreateItem(int itemID)
    {
        Item temp = new global::Item();
        string name = "";
        int value = 0;
        string description = "";
        string icon = "";
        string mesh = "";
        ItemType type = ItemType.Food;

        // A list of every item in the game, each with an unique identity number
        #region Item list
        switch (itemID)
        {
            // The items are classified by number which we can refer to in the inventory
            #region Food 0-99
            case 0:
                name = "Meat";
                value = 5;
                description = "Tastes like Chicken";
                icon = "Meat";
                mesh = "Meat";
                type = ItemType.Food;
                break;
            case 1:
                name = "Apple";
                value = 5;
                description = "Chicken after taste";
                icon = "Apple";
                mesh = "Apple";
                type = ItemType.Food;
                break;
            default:
                itemID = 1;
                name = "Apple";
                value = 5;
                description = "Munchies";
                icon = "Apple";
                mesh = "Apple";
                type = ItemType.Food;
                break;
            #endregion
            #region Weapon 100-199
            case 100:
                name = "Axe";
                value = 15;
                description = "Your only weapon really";
                icon = "Axe";
                mesh = "Axe";
                type = ItemType.Weapon;
                break;
            case 101:
                name = "Bow";
                value = 100;
                description = "Aim and fire";
                icon = "Bow";
                mesh = "Bow";
                type = ItemType.Weapon;
                break;
            case 102:
                name = "Sword";
                value = 100;
                description = "Slice and dice";
                icon = "Sword";
                mesh = "Sword";
                type = ItemType.Weapon;
                break;
            case 103:
                name = "Shield";
                value = 200;
                description = "Make shift sled";
                icon = "Shield";
                mesh = "Shield";
                type = ItemType.Weapon;
                break;
            #endregion
            #region Apparel 200-299
            case 200:
                name = "Pants";
                value = 1;
                description = "Wash them now";
                icon = "Pants";
                mesh = "Pants";
                type = ItemType.Apparel;
                break;
            case 201:
                name = "Belt";
                value = 2;
                description = "Self tightening snake";
                icon = "Belt";
                mesh = "Belt";
                type = ItemType.Apparel;
                break;
            case 202:
                name = "Armour";
                value = 20;
                description = "Itchy";
                icon = "Armour";
                mesh = "Armour";
                type = ItemType.Apparel;
                break;
            case 203:
                name = "Boots";
                value = 12;
                description = "Steel cap";
                icon = "Boots";
                mesh = "Boots";
                type = ItemType.Apparel;
                break;
            case 204:
                name = "Shouldes";
                value = 8;
                description = "Comes in pairs";
                icon = "Shoulders";
                mesh = "Shoulders";
                type = ItemType.Apparel;
                break;
            case 205:
                name = "Cloak";
                value = 0;
                description = "A Curse";
                icon = "Cloak";
                mesh = "Cloak";
                type = ItemType.Apparel;
                break;
            case 206:
                name = "Braces";
                value = 30;
                description = "Makes you strong somehow";
                icon = "Braces";
                mesh = "Braces";
                type = ItemType.Apparel;
                break;
            case 207:
                name = "Helmet";
                value = 50;
                description = "XL for bigheads";
                icon = "Helmet";
                mesh = "Helmet";
                type = ItemType.Apparel;
                break;
            case 208:
                name = "Gloves";
                value = 20;
                description = "Sticky";
                icon = "Gloves";
                mesh = "Gloves";
                type = ItemType.Apparel;
                break;
            case 209:
                name = "Necklace";
                value = 100;
                description = "Cosmetic only";
                icon = "Necklace";
                mesh = "Necklace";
                type = ItemType.Apparel;
                break;
            case 210:
                name = "Ring";
                value = 1000;
                description = "0.5 karat gold";
                icon = "Ring";
                mesh = "Ring";
                type = ItemType.Apparel;
                break;
            #endregion
            #region Crafting 300-399
            case 300:
                name = "Ingot";
                value = 10;
                description = "Do not eat";
                icon = "Ingot";
                mesh = "Ingot";
                type = ItemType.Crafting;
                break;
            case 301:
                name = "Gem";
                value = 500;
                description = "sell";
                icon = "Gem";
                mesh = "Gem";
                type = ItemType.Crafting;
                break;
            #endregion
            #region Quest 400-499
            #endregion
            #region Money 500-599
            case 500:
                name = "Coins";
                value = 10;
                description = "A start";
                icon = "Coins";
                mesh = "Coins";
                type = ItemType.Money;
                break;
            #endregion
            #region Ingredients 600-699
            #endregion
            #region potions 700-799
            case 700:
                name = "HP";
                value = 30;
                description = "Vital";
                icon = "HP";
                mesh = "HP";
                type = ItemType.Potions;
                break;
            case 701:
                name = "MP";
                value = 1;
                description = "Not as Vital";
                icon = "MP";
                mesh = "MP";
                type = ItemType.Potions;
                break;
            #endregion
            #region Scroll 800-899
            case 800:
                name = "Scroll";
                value = 100;
                description = "Homage to Fromage";
                icon = "Scroll";
                mesh = "Scroll";
                type = ItemType.Scrolls;
                break;
            case 801:
                name = "Book";
                value = 10;
                description = "How to save your game";
                icon = "Book";
                mesh = "Book";
                type = ItemType.Scrolls;
                break;
                #endregion
        }
        #endregion
        temp.ID = itemID;
        temp.Name = name;
        temp.Value = value;
        temp.Description = description;
        temp.Icon = Resources.Load("Icons/" + icon) as Texture2D;
        temp.Mesh = mesh;
        temp.Type = type;
        return temp;
    }
}
