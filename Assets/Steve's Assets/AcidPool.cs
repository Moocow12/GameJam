using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour {

    public int damage = 25;
    public float effectRadius = 1f;
    public float duration = 4f;
    private float durationCD;

    public float tickRate = .5f;
    private float tickRateCD;


	// Use this for initialization
	void Start ()
    {
        durationCD = duration;
        tickRateCD = tickRate;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Acid();
	}

    private void Acid()
    {
        if (durationCD > 0 )
        {
            //print(durationCD);
            durationCD -= Time.deltaTime;
            if (tickRateCD > 0)
            {
                tickRateCD -= Time.deltaTime;
            }
            else if (tickRateCD <= 0)
            {
                tickRateCD = tickRate;
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, effectRadius);
                foreach (Collider2D col in colliders)
                {
                    if (col.GetComponent<Enemy>())
                        col.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        }
        else if (durationCD <= 0)
        {
            Destroy(gameObject);
        }
    }
}
