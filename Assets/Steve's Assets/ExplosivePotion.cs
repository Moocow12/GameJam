using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivePotion : Projectile {

    public float blastRadius = 1f;

	// Use this for initialization
	void Start () {
        lifeTimeCD = lifeTime;
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
            Break();        // break on impact with an enemy
        }

        hasLanded = true;
    }

    new protected void Countdown()
    {
        lifeTimeCD -= Time.deltaTime;       // countdown lifeTimeCD each frame
        if (lifeTimeCD <= 0)        // if the lifeTime has run out...
        {
            Break();        // the potion breaks
        }
    }

    new private void Break()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        foreach (Collider2D col in colliders)
        {
            if (col.GetComponent<Enemy>())
                col.GetComponent<Enemy>().TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }
}
