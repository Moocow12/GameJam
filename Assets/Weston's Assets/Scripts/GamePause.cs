using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{

    public void PauseGame()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in objects)
        {
            Enemy script = obj.GetComponent<Enemy>();
            if(script != null)
            {
                script.enabled = false;
            }
        }
        GameObject spawner = GameObject.Find("EnemySpawner");
        if(spawner != null)
        {
            spawner.GetComponent<EndlessSpawner>().enabled = false;
        }
     
    }


    public void ResumeGame()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in objects)
        {
            Enemy script = obj.GetComponent<Enemy>();
            if (script != null)
            {
                script.enabled = true;
            }
        }
        GameObject spawner = GameObject.Find("EnemySpawner");
        if (spawner != null)
        {
            spawner.GetComponent<EndlessSpawner>().enabled = true;
        }
    }
}
