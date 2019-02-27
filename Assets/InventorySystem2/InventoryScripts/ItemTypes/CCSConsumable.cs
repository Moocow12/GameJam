using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCS;


public class CCSConsumable : CCSUsable
    {

        
        public override CCSItemType ItemType
        {
            get
            {
                return CCSItemType.CCSConsumable;
            }
        }

        /// <summary>
        /// Is dependant on how you set up your Hover Over Object.
        /// Use Rich Text formatting or HTML to allow for different colors, Fonts and sizes to be used.
        /// </summary>
        /// <returns></returns>
        public override string DisplayInfo()
        {
            return base.DisplayInfo();
        }


        /// <summary>
        /// Allows for the funtionality built into the item to be performed. 
        /// Modify this script in deriving classes for modified functionality.
        /// </summary>
        /// <param name="slot"></param>
        public override void Use(CCSSlot slot)
        {
            base.Use(slot);
            slot.RemoveItem();
        }
    }


