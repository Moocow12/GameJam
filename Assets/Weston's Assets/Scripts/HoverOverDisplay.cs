using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HoverOverDisplay : MonoBehaviour
{
    public TextMeshProUGUI itemName, description;
    public Image icon;
    public Sprite defaultIcon;
    


    public void DisplayItem(Item item)
    {
        icon.sprite = item.inventoryIcon;
        itemName.text = item.name;
        description.text = item.description;
    }

    public void ResetDisplay()
    {
        icon.sprite = defaultIcon;
        itemName.text = "";
        description.text = "";
    }
}
