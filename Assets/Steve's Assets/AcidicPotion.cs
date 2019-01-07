using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behavior", menuName = "Potion Behaviour/AcidicPotion")]
public class AcidicPotion : BreakBehaviour {

    public AcidPool acid;


    public override void Break(Vector2 collisionPosition, GameObject collision)
    {
        
        Instantiate(acid, new Vector3(collisionPosition.x, -2.6f), new Quaternion(0, 0, 0, 0));
    }
}
