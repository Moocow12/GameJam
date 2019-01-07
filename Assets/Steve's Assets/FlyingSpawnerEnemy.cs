using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSpawnerEnemy : Enemy {

    public Enemy enemyPrefab;
    public float spawnInterval = 4f;

    private float spawnIntervalCD;

	// Use this for initialization
	void Start () {
        spawnIntervalCD = spawnInterval;
        rbody = this.GetComponent<Rigidbody2D>();
        if (isFlying == true)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
		if (spawnIntervalCD > 0)
        {
            spawnIntervalCD -= Time.deltaTime;
        }
        else if (spawnIntervalCD <= 0)
        {
            SpawnEnemy();
            spawnIntervalCD = spawnInterval;
        }

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

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, this.transform.position, this.transform.rotation);
    }
}
