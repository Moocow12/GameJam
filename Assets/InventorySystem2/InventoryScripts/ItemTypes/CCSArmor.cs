using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CCS;



[CreateAssetMenu(fileName = "New Armor Item", menuName = "CCS/Items/Armor")]
    public class CCSArmor : CCSEquipment
    {


        //Armor Type is used within the CCSArmorSlot class to determine what is to be done with this type of item
        public CCSArmorType armorType;


        //Other stat information that is available only to the Armor type items can go here.
        public int armor;


        /// <summary>
        /// Is dependant on how you set up your Hover Over Object.
        /// Use Rich Text formatting or HTML to allow for different colors, Fonts and sizes to be used.
        /// </summary>
        /// <returns></returns>
        public override string DisplayInfo()
        {
            string info =  base.DisplayInfo();
            //info += all information in this class.
            return info;
            
        }

        /**
         * 
         * public override void UpdateCharacterInfo()
         * {
         *      base.UpdateCharacterInfo();
         *      Talk to your character manager and implement the necessary functionality to account for the stat information that was added in this class.
         * }
         * 
         **/

    }
