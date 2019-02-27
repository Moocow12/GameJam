using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCS;

public interface ICCSItem
{
       


    /// <summary>
    /// All items will have a use function that will determine what the item can do when clicked on in the inventory. 
    /// </summary>
    /// <param name="slot">Contains all the information about the slot and inventory the item is in.</param>
    void Use(CCSSlot slot);
    /// <summary>
    ///Gives a rich text string that allows for custom colors, fonts and values of the item.
    /// </summary>
    string DisplayInfo();

}

