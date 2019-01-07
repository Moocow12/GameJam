using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{

    public List<Item> items;

    public Image _icon;
    public TextMeshProUGUI _text;
    public Sprite defaultIcon;
    Item itemTypeTester = null;

    Inventory inventoryParent;
    HoverOverDisplay hoverOver;


    private void Start()
    {
        hoverOver = FindObjectOfType<HoverOverDisplay>();
        inventoryParent = GetComponentInParent<Inventory>();
        UpdateCount();
        UpdateIcon();
    }

    private void Update()
    {
        UpdateCount();
        UpdateIcon();


    }

    /// <summary>
    /// Adds the desired to the inventory, Check to see if the inventory is full before using this method or you could lose the item.
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        if (items.Count > 0)
        {
            if (items[0].stackSize > items.Count)
            {
                items.Add(item);
            }
        }
        else
        {
            items.Add(item);
        }
        UpdateIcon();
        UpdateCount();
    }

    public string CurrentItemName()
    {
        if (!IsEmpty())
        {
            return items[0].name;
        }
        return "";
    }
    /// <summary>
    /// Removes an item from the inventory if possible
    /// </summary>
    public void RemoveItem()
    {
        if (!IsEmpty())
        {
            items.RemoveAt(0);
        }
        UpdateIcon();
        UpdateCount();
    }

    /// <summary>
    /// Checks to see if the slot is currently Empty
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        if (items.Count == 0)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// Determines if the slot is currently at the stack capacity 
    /// </summary>
    /// <returns></returns>
    public bool IsFull()
    {
        if (IsEmpty())
        {
            return false;
        }
        else if (items[0].stackSize > items.Count)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Displays the current count of the item.
    /// </summary>
    /// <returns></returns>
    public int CurrentCount()
    {
        return items.Count;
    }


    /// <summary>
    /// Changes the icon within the inventory to what item is or is not in the slot.
    /// </summary>
    public void UpdateIcon()
    {
        if (_icon != null)
        {
            if (!IsEmpty())
            {
                _icon.sprite = items[0].inventoryIcon;
            }
            else
            {
                _icon.sprite = defaultIcon;
            }
        }
    }

    public void ClearSlot()
    {
        items.Clear();
        UpdateIcon();
        UpdateCount();
    }
    /// <summary>
    /// Changes the Text within the inventory to how many items are left if the number is greater than 0
    /// </summary>
    public void UpdateCount()
    {
        if (_text != null)
        {
            if (!IsEmpty())
            {
                _text.text = CurrentCount().ToString();
            }
            else
            {
                _text.text = "";
            }
        }
    }



  

    public void SlotClick()
    {
        InventoryManager mng = FindObjectOfType<InventoryManager>();
        //mng.SlotClick(items,SlotType());
        mng.SlotClick(this);
    }

    public InventoryType SlotType()
    {
        return inventoryParent.type;
    }


    Coroutine mouseHolder;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(SlotType() == InventoryType.Crafter)
        {
            mouseHolder = StartCoroutine(WaitForClick());
            Debug.Log("Entering");
        }
        if(!IsEmpty())
        {
            hoverOver.DisplayItem(items[0]);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(SlotType() == InventoryType.Crafter)
        {
            StopCoroutine(mouseHolder);
            Debug.Log("Exiting");
        }

        hoverOver.ResetDisplay();
    }


    IEnumerator WaitForClick()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(1));
        items = new List<Item>();

    }

    public ItemType GetItemType()
    {
        if(items.Count >0)
        {
            return items[0].itemType;
        }
        return ItemType.None;
    }
}
