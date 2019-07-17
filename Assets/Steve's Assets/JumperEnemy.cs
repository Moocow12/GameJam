using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperEnemy : Enemy {

    public float jumpForce = 1f;
    public float jumpRate = 2f;

    private float jumpRateCD;

	// Use this for initialization
	void Start () {
        rbody = this.GetComponent<Rigidbody2D>();
        constraints = rbody.constraints;
        jumpRateCD = jumpRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (jumpRateCD > 0)
        {
            jumpRateCD -= Time.deltaTime;
        }
        else
        {
            Jump();
            jumpRateCD = jumpRate;
        }

        if (poisonDuration > 0)
        {
            ProcessPoison();
        }

        if (freezeDuration > 0)
        {
            freezeDuration -= Time.deltaTime;
            rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (freezeDuration <= 0 && rbody.constraints == RigidbodyConstraints2D.FreezeAll)
        {
            rbody.constraints = constraints;
        }
    }

    private void Jump()
    {
        rbody.AddForce(new Vector2(-1f, 3f) * jumpForce);
    }
}
