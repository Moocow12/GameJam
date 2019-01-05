using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Launcher : MonoBehaviour {

    public GameObject potionPrefab;
    public float forceMultiplyer = 1f;
    public Slider powerBar;
    private float force = 1f;
    private Vector3 angle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.Mouse0) == true)
        {
            LaunchProjectile();
        }
        CalculateProjectileVector();
	}

    private void LaunchProjectile()
    {
        GameObject projectile = Instantiate(potionPrefab, this.transform.position, this.transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce((angle * (force * -1)), ForceMode2D.Force);
    }

    private void CalculateProjectileVector()
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        position = Camera.main.ScreenToWorldPoint(position);
        position = position - this.gameObject.transform.position;

        force = (Mathf.Abs(position.x) + Mathf.Abs(position.y)) * forceMultiplyer;
        force = Mathf.Clamp(force, 0f, 200f);
        powerBar.value = force;
        print("Current force: " + force);

        angle = position;
        //print(angle);
    }
}
