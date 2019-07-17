using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDodgerEnemy : Enemy {

    public float verticalForce = 1f;
    public float dodgeInterval = 3f;

    private float dodgeIntervalCD;
    private bool movingUp = true;

	// Use this for initialization
	void Start ()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        constraints = rbody.constraints;
        dodgeIntervalCD = dodgeInterval;
        if (isFlying == true)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();         // move forward at a constant pace

        if (dodgeIntervalCD > 0 && movingUp == true)        // if we should still be moving up...
        {
            ApplyVerticalForce(1f);         // apply a posative vertical force
            dodgeIntervalCD -= Time.deltaTime;
        }
        else if (dodgeIntervalCD > 0 && movingUp == false)      // if we should still be moving down...
        {
            ApplyVerticalForce(-1f);        // apply a negative vertical force
            dodgeIntervalCD -= Time.deltaTime;
        }
        else if (dodgeIntervalCD <= 0)      // if the dodge interval has completed...
        {
            dodgeIntervalCD = dodgeInterval;        // reset the dodge interval
            movingUp = !movingUp;       // change dodge directions
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

    private void ApplyVerticalForce(float value)
    {
        rbody.AddForce(Vector2.up * value * verticalForce);
    }
}
