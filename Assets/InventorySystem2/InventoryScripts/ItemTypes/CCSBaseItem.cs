using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCS;



    /// <summary>
    /// If using Custom Item class scriptable objects, for inventory usage please derive from this class.
    /// </summary>

    [CreateAssetMenu(fileName = "New Basic Item", menuName = "CCS/Items/Basic Item")]
    public class CCSBaseItem : ScriptableObject, ICCSItem
    {


        // NOTE: DO NOT REMOVE 'itemID', it is required for functionality built into almost all classes in the inventory.
        public int itemID;


        //Basic information variables about the item.
        public string itemName;
        public int stackSize;
        public Sprite icon;


        /// <summary>
        /// Changes the srpite into a texture2D for use as a mouse cursor.
        /// </summary>
        public Texture2D CursorIcon
        {
            get
            {
                return icon.texture;
            }
        }

        /// <summary>
        /// Item type that is based on the type of item it is.
        /// Used within the Inventory as to what items can be accepted.
        /// </summary>
        public virtual CCSItemType ItemType
        {
            get { return CCSItemType.CCSBaseItem; }
        }


        /// <summary>
        /// Allows for the funtionality built into the item to be performed. 
        /// Modify this script in deriving classes for modified functionality.
        /// </summary>
        /// <param name="slot"></param>
        public virtual void Use(CCSSlot slot)
        {

        }



        /// <summary>
        /// Is dependant on how you set up your Hover Over Object.
        /// Use Rich Text formatting or HTML to allow for different colors, Fonts and sizes to be used.
        /// </summary>
        /// <returns></returns>
        public virtual string DisplayInfo()
        {
            string info = itemName;
            //info += all information in this class.
            return info;
        }
    }
 