
namespace CCS
{

    /**
        Items that are used within the inventory system derive from the base class 'CCSBaseItem'.
        If creating a new type of item please make sure that you add the class name to 'CCSItemType' as a flag enum. 
        Be sure to spell the class name exactly!
    **/
    public enum CCSItemType
    {
        CCSBaseItem = 1,
        CCSUsable = 2,
        CCSConsumable = 4,
        CCSEquipment = 8,
        CCSAbility = 16,
        OffensivePotion = 32,
        DefensivePotion = 64,
        //Addition Example: Multiply the previous number by 2
        //CCSPotion = 32,
        //CCSTester = 64,
    }

}

