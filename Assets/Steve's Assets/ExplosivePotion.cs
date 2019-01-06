using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivePotion : Projectile {

    private CircleCollider2D explosionRadius;
    private float explosionDuration = .1f;
    private float explosionDurationCD;

	// Use this for initialization
	void Start () {
        explosionDurationCD = explosionDuration;
        explosionRadius = GetComponentInChildren<CircleCollider2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if (hasLanded == true)          // if the potion has landed on a surface...
        {
            Countdown();        // call Countdown()
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())         // if we collided with an enemy...
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);      // deal damage to it
            Break();        // break on impact with an enemy
        }

        hasLanded = true;
    }
}
