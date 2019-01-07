using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 2;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            // GAME OVER
            print("GAME OVER");
            SceneManager.LoadScene(0);

        }
        else if (health <= 1)
        {
            // TURN SLIGHTLY RED
        }
    }

    public void Heal()
    {
        health++;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health > 1)
        {
            // RETURN TO NORMAL COLOR
        }
    }
}
