using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace CCS
{



    public class CCSInventory : MonoBehaviour
    {

        [InformationButton("Inventory Info", "Inventory", "The CCSInventory is a holding class that organizes and communicates with" +
            " the CCSSlot class. The slots are set up in the heirarchy as children of the inventory game object and can only function in that regard." +
            "\n\nNotice: There are numbers on the slots within the inventory and increase as more are added. This is the designation number to which slot will" +
            " recieve an item first on pickup. Reordering of this list is done by moving the CCSSlot gameobjects in the hierarchy above one another.")]
        public int PriorityLevel;

        [InformationButton("Type Meaning", "Type",
            "Priority Level: If multiple inventories of the same Inventory type are available on pickup, priority level determines which Inventory the item will try to go in first. (Lower is Better)\n\n"+
            "Inventory Type: Is used for pickup navigation on loot and equipment items.\n\n" +
            "Accepted Item Types: Flag used to determine what items are able to be stored within the Inventory. \n\n" +
            "Usable Item Types: What items are able to have their Use(); called if the slot is designated with the functionality.")]
        [SerializeField]
        private CCSInventoryType inventoryType;//Used with auto loot search functions.


        [EnumFlags]
        [SerializeField]
        private CCSItemType acceptedItemTypes;//The type of items that the inventory can hold.

        [EnumFlags]
        [SerializeField]
        private CCSItemType useableItemTypes;//Types of items that can call the use function within the inventory.


        public CCSInventoryType InventoryType
        {
            get
            {
                return inventoryType;
            }

        }

        public CCSItemType AcceptedItemTypes
        {
            get
            {
                return acceptedItemTypes;
            }


        }

        public CCSItemType UseableItemTypes
        {
            get
            {
                return useableItemTypes;
            }

        }

        [HideInInspector]
        public CCSSlot[] slots;



        // Start is called before the first frame update
        void Start()
        {

            CCSSlot[] slots = GetComponentsInChildren<CCSSlot>();
            
            foreach(CCSSlot slot in slots)
            {
                slot.InitializeSlot(this);
            }

        }

        // Update is called once per frame
        void Update()
        {

        }




        private void OnDrawGizmos()
        {
            slots = GetComponentsInChildren<CCSSlot>();
            for (int i = 0; i < slots.Length; i++)
            {
                GUIStyle style = new GUIStyle();
                style.fontStyle = FontStyle.Bold;
                style.fontSize = 15;
                style.normal.textColor = Color.black;
                
                Handles.Label(slots[i].transform.position, i.ToString(),style);
            }
        }
    }
}