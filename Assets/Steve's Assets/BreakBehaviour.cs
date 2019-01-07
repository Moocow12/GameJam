using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BreakBehaviour : ScriptableObject
{
    public int damage;

    public virtual void Break(Vector2 collisionPosition, GameObject collision)
    {

    }
}
