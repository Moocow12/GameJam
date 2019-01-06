using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage = 0;
    public BreakBehaviour breakBehaviour;
    public float lifeTime = 3f;
    protected float lifeTimeCD;
    protected bool hasLanded = false;

	// Use this for initialization
	void Start () {
        lifeTimeCD = lifeTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasLanded == true)          // if the potion has landed on a surface...
        {
            Countdown();        // call Countdown()
        }
	}

    protected void Break()
    {
        breakBehaviour.Break();
        Destroy(this.gameObject);
    }

    protected void Countdown()
    {
        lifeTimeCD -= Time.deltaTime;       // countdown lifeTimeCD each frame
        if (lifeTimeCD <= 0)        // if the lifeTime has run out...
        {
            Break();        // the potion breaks
        }
    }

    public void Initialize(Item item)
    {
        this.damage = item.damage;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = item.inventoryIcon;
        this.breakBehaviour = item.breakBehaviour;
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
