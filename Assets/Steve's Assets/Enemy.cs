using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;
    public float moveSpeed = 10f;
    public int damage = 5;
    public bool isFlying = false;

    protected Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
        rbody = this.GetComponent<Rigidbody2D>();
        if (isFlying == true)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);
        }
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    protected void Movement()
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
