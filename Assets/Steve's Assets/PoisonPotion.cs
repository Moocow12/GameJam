using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPotion : Projectile {

    public float poisonDuration;
    public float poisonTickRate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Poison(damage, poisonDuration, poisonTickRate);
        }
        Break();
    }

    new private void Break()
    {
        Destroy(gameObject);
    }
}
