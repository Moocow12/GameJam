using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCS;


    [CreateAssetMenu(fileName = "New Ability", menuName = "CCS/Items/Ability")]
    public class CCSAbility : CCSUsable
    {

        public override CCSItemType ItemType
        {
            get
            {
                return CCSItemType.CCSAbility;
            }
        }


        /// <summary>
        /// Is dependant on how you set up your Hover Over Object.
        /// Use Rich Text formatting or HTML to allow for different colors, Fonts and sizes to be used.
        /// </summary>
        /// <returns></returns>
        public override string DisplayInfo()
        {
            string info = base.DisplayInfo();
            //info += all information in this class.
            return info;

        }
    }

