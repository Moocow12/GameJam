using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawner : MonoBehaviour
{

    public Enemy[] possibleEnemies;         // MUST BE THE SAME SIZE AS FREQUENCY
    public float[] frequency;       // MUST BE THE SAME SIZE AS POSSIBLEENEMIES
    private float[] frequencyCD = new float[6];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < frequency.Length; i ++)
        {
            frequencyCD[i] = frequency[i];
            //print(frequencyCD[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < possibleEnemies.Length; i++)
        {
            if (frequencyCD[i] > 0)
            {
                frequencyCD[i] -= Time.deltaTime;
            }
            else if (frequencyCD[i] <= 0)
            {
                //print(possibleEnemies[i].gameObject.name + " spawned");
                Instantiate(possibleEnemies[i].gameObject, transform.position, transform.rotation);
                frequencyCD[i] = frequency[i];
                if (i == possibleEnemies.Length - 1)
                {
                    for (int o = 0; o < frequency.Length; o++)
                    {
                        frequency[o] *= .8f;
                        //print("Frequency decreased");
                    }
                }
            }
        }
    }
}
