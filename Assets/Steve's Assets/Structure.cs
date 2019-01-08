using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour {

    public int initialHeight = 6;
    public WallBlock wallBlockPrefab;
    
    //private int health = 2;
    private Vector2 currentTop;
    private Stack<WallBlock> wall = new Stack<WallBlock>();

	// Use this for initialization
	void Start () {

        currentTop = this.transform.position;
		for (int i = 0; i < initialHeight; i ++)
        {
            wall.Push(Instantiate(wallBlockPrefab, currentTop, RandomRotation(), this.gameObject.transform));
            currentTop.y += .56f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private Quaternion RandomRotation()
    {
        float rotation = 0;
        int value = Random.Range(0, 4);

        if (value == 0)
            rotation = 0;
        else if (value == 1)
            rotation = 90;
        else if (value == 2)
            rotation = 180;
        else if (value == 3)
            rotation = 270;

        return new Quaternion(0, 0, rotation, 0);
    }

    public void AddBlock()
    {
        if (wall.Count <= 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            wall.Push(Instantiate(wallBlockPrefab, currentTop, RandomRotation(), this.gameObject.transform));
        }
        else
        {
            wall.Peek().SetHealth(2);
            wall.Push(Instantiate(wallBlockPrefab, currentTop, RandomRotation(), this.gameObject.transform));
        }
        
        currentTop.y += .56f;
    }

    public void TakeDamage()
    {

        wall.Peek().SetHealth(wall.Peek().GetHealth() - 1);
        //print("Top block health: " + wall.Peek().GetHealth());
        if (wall.Peek().GetHealth() <= 0 && wall.Count > 0)
        {
            //print("Top block destroyed");
            currentTop.y -= .56f;
            Destroy(wall.Pop().gameObject);
            if (wall.Count == 0)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            wall.Peek().gameObject.GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
        }
    }
}
