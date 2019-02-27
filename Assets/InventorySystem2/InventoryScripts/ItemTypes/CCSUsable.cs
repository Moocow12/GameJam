using System.Collections;
using System.Collections.Generic;
using CCS;
using UnityEngine;

public class CCSUsable : CCSBaseItem
{
    public override CCSItemType ItemType
    {
        get
        {
            return CCSItemType.CCSUsable;
        }
    }

    public override string DisplayInfo()
    {
        return base.DisplayInfo();
    }

    public override void Use(CCSSlot slot)
    {
        Debug.Log("Used Item");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
