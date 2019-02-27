using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CCS
{
    public class CCSWeaponSlot : CCSSlot
    {
        [EnumFlags]
        public CCSWeaponType acceptedWeaponTypes;

    }
}
