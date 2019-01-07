using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;
    public float moveSpeed = 10f;
    public int damage = 5;
    public bool isFlying = false;
    public int[] dropTable = new int[11];
    public ItemList itemList;

    protected Rigidbody2D rbody;

    protected float poisonDuration = 0;
    protected float poisontickRate = 0;
    protected float poisontickRateCD = 0;
    protected int poisonDamage = 0;

    protected float freezeDuration;
    protected RigidbodyConstraints2D constraints;

	// Use this for initialization
	void Start () {
        rbody = this.GetComponent<Rigidbody2D>();
        constraints = rbody.constraints;
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

        if (freezeDuration > 0)
        {
            freezeDuration -= Time.deltaTime;
            rbody.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else if (freezeDuration <= 0)
        {
            rbody.constraints = constraints;
        }
	}

    protected void ProcessPoison()
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

        Instantiate(itemList.itemList[RandomItem()], new Vector3(transform.position.x, -2f), transform.rotation);
        Destroy(this.gameObject);       // remove this game object from the scene
    }

    public void Poison(int damage, float duration, float tickRate)
    {
        poisonDamage = damage;
        poisonDuration = duration;
        poisontickRate = tickRate;
        poisontickRateCD = poisontickRate;
    }

    public void Freeze(float duration)
    {
        print("Frozen");
        freezeDuration = duration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Structure>())       // if we have just collided with a structure...
        {
            collision.gameObject.GetComponent<Structure>().TakeDamage();      // deal damage to the structure
            StartCoroutine(Die());
        }
        else if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().TakeDamage();
            StartCoroutine(Die());
        }
    }

    private int RandomItem()
    {
        int range = 0;
        for (int i = 0; i < dropTable.Length; i++)
        {
            range += dropTable[i];
        }

        int randomValue = Random.Range(0, range);
        int top = 0;

        for (int i = 0; i < dropTable.Length; i++)
        {
            top += dropTable[i];
            if (randomValue < top)
            {
                return i;
            }
        }
        return dropTable.Length - 1;
    }

}
