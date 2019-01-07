using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlock : MonoBehaviour
{
    [SerializeField] private int health;
    public int GetHealth() { return health; }
    public void SetHealth(int value) { health = value; }
}
