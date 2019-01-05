using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;
    public float moveSpeed = 10f;
    public int damage = 5;

    private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
        rbody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    private void Movement()
    {
        rbody.AddForce(Vector2.left * moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;       // subtract damage from health
        if (health <= 0)        // if the remaining health is <= 0...
        {
            Die();          // this unit will die
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);       // remove this game object from the scene
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Structure>())       // if we have just collided with a structure...
        {
            collision.gameObject.GetComponent<Structure>().TakeDamage(damage);      // deal damage to the structure
            Die();
        }
    }
}
