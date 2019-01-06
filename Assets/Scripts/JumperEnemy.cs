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
	}

    private void Jump()
    {
        rbody.AddForce(new Vector2(-1f, 3f) * jumpForce);
    }
}
