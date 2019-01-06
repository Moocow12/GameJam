using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;
    public float moveSpeed = 10f;
    public int damage = 5;
    public bool isFlying = false;

    protected Rigidbody2D rbody;

    private float poisonDuration = 0;
    private float poisontickRate = 0;
    private float poisontickRateCD = 0;
    private int poisonDamage = 0;

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

        if (poisonDuration > 0)
        {
            ProcessPoison();
        }
	}

    private void ProcessPoison()
    {
        poisonDuration -= Time.deltaTime;
        if (poisontickRateCD > 0)
        {
            poisontickRateCD -= Time.deltaTime;
        }
        else if (poisontickRateCD <= 0)
        {
            TakeDamage(poisonDamage);
            poisontickRateCD = poisontickRate;
        }
    }

    protected void Movement()
    {
        rbody.AddForce(Vector2.left * moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;       // subtract damage from health
        StartCoroutine(ShowHit());
        if (health <= 0)        // if the remaining health is <= 0...
        {
            StartCoroutine(Die());          // this unit will die
        }
    }

    protected IEnumerator ShowHit()
    {
        float ElapsedTime = 0;
        float TotalTime = .1f;
        Color savedColor = this.GetComponentInChildren<SpriteRenderer>().color;
        while (ElapsedTime < TotalTime)
        {
            //Debug.Log("Starting Infestation!");
            ElapsedTime += Time.deltaTime;
            //this.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(new Color(1, 1, 1, 1f), new Color(1, 0, 0, 1f), (ElapsedTime / TotalTime));  // for when enemies have sprites
            this.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(savedColor, new Color(1, 0, 0, 1f), (ElapsedTime / TotalTime));            // temporary use while enemies dont have sprites
            yield return new WaitForEndOfFrame();
        }
        ElapsedTime = 0;
        while (ElapsedTime < TotalTime)
        {
            ElapsedTime += Time.deltaTime;
            //this.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(new Color(1, 0, 0, 1f), new Color(1, 1, 1, 1f), (ElapsedTime / TotalTime));  // for when enemies have sprites
            this.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(new Color(1, 0, 0, 1f), savedColor, (ElapsedTime / TotalTime));            // temporary use while enemies dont have sprites
            yield return new WaitForEndOfFrame();
        }
        //Debug.Log("Ending Infestation!");
    }

    private IEnumerator Die()
    {
        float ElapsedTime = 0;
        float TotalTime = .3f;
        Color savedColor = this.GetComponentInChildren<SpriteRenderer>().color;
        while (ElapsedTime < TotalTime)
        {
            //Debug.Log("Starting Infestation!");
            ElapsedTime += Time.deltaTime;
            //this.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(new Color(1, 1, 1, 1f), new Color(1, 0, 0, 1f), (ElapsedTime / TotalTime));  // for when enemies have sprites
            this.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(savedColor, new Color(savedColor.r, savedColor.g, savedColor.b, 0), (ElapsedTime / TotalTime));            // temporary use while enemies dont have sprites
            yield return new WaitForEndOfFrame();
        }

        Destroy(this.gameObject);       // remove this game object from the scene
    }

    public void Poison(int damage, float duration, float tickRate)
    {
        poisonDamage = damage;
        poisonDuration = duration;
        poisontickRate = tickRate;
        poisontickRateCD = poisontickRate;
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
