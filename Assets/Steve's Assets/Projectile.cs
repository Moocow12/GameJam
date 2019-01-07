using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage = 0;
    public BreakBehaviour breakBehaviour;
    public float lifeTime = 3f;
    protected float lifeTimeCD;
    protected bool hasLanded = false;


    protected Vector2 collisionPoint;
    protected GameObject collision;
    protected GameObject prefabObjectOnBreak;
    protected AudioClip soundToPlayOnImpact;
    protected float prefabDestructionTime;
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
        AudioSource audio = GameObject.Find("PotionBreakAudio").GetComponent<AudioSource>();
        if(audio != null)
        {
            Debug.Log("playingClip");
            audio.clip = soundToPlayOnImpact;
            audio.Play();
        }
        else
        {
            Debug.Log("Please add the Prefab 'PotionBreakAudio' to the game Scene");
        }
        if(breakBehaviour != null)
        {
            breakBehaviour.Break(collisionPoint, collision);
        }
        if(prefabObjectOnBreak != null)
        {
            GameObject obj = Instantiate(prefabObjectOnBreak, collisionPoint, Quaternion.identity);
            Destroy(obj, prefabDestructionTime);
        }

        Destroy(gameObject);
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
        prefabDestructionTime = item.prefabDestructionTime;
        prefabObjectOnBreak = item.objectPrefabOnBreak;
        soundToPlayOnImpact = item.breakAudioClip;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = item.inventoryIcon;
        this.breakBehaviour = item.breakBehaviour;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D point = collision.GetContact(0);
        collisionPoint = point.point;
        this.collision = collision.gameObject;
        if (collision.gameObject.GetComponent<Enemy>() )         // if we collided with an enemy...
        {

            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);      // deal damage to it
            
            Break();        // break on impact with an enemy
        }
        if(collision.gameObject.CompareTag("Ground"))
        {
            Break();
        }
        
        hasLanded = true;
    }
}
