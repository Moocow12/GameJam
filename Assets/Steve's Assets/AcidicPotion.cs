using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behavior",menuName = "Potion Behaviour")]
public class AcidicPotion : BreakBehaviour {

    public AcidPool acid;


    public override void Break(Vector2 collisionPosition)
    {
        
        Instantiate(acid,collisionPosition, new Quaternion(0, 0, 0, 0));
    }
}
