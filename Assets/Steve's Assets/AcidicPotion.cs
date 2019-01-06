using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidicPotion : Projectile {

    public AcidPool acid;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Break();
        }
    }

    new private void Break()
    {
        Instantiate(acid, transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(gameObject);
    }
}
