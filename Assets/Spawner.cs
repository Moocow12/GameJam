using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave : System.Object
{
    public Enemy[] enemies;
    public float spawnInterval = .75f;

    private int enemyCounter = 0;
    public int GetEnemyCounter() { return enemyCounter; }
    public void SetEnemyCounter(int value) { enemyCounter = value; }

    private float spawnIntervalCD = 1.5f;
    public float GetSpawnCD() { return spawnIntervalCD; }

    public void SetSpawnCD(float value)
    {
        spawnIntervalCD = value;
    }
}

public class Spawner : MonoBehaviour {

    public Wave[] enemyWaves;

    private int waveCounter = 0;
    private Wave currentWave;
    private bool doneSpawning = false;

	// Use this for initialization
	void Start () {
        currentWave = enemyWaves[waveCounter];
	}
	
	// Update is called once per frame
	void Update () {
		if (doneSpawning == false)
        {
            ProcessWaves();
        }
	}

    private void ProcessWaves()
    {
        
        if (currentWave.GetSpawnCD() > 0)       // if we're waiting to spawn something...
        {
            currentWave.SetSpawnCD(currentWave.GetSpawnCD() - Time.deltaTime);        // countdown the spawn interval every frame
        }
        else if (currentWave.GetSpawnCD() <= 0 && currentWave.GetEnemyCounter() < currentWave.enemies.Length)         // if it's time to spawn something, and we have something else to spawn...
        {
            print("Enemy Spawned");
            //print("WaveCounter: " + waveCounter + ", enemyWaves.Length: " + enemyWaves.Length);
            Instantiate(currentWave.enemies[currentWave.GetEnemyCounter()]);        // instantiate the current enemy on the current wave
            currentWave.SetSpawnCD(currentWave.spawnInterval);      // reset the spawn interval
            currentWave.SetEnemyCounter(currentWave.GetEnemyCounter() + 1);         // increment the enemy counter of the current wave
        }
        else if (currentWave.GetEnemyCounter() >= currentWave.enemies.Length && waveCounter + 1 < enemyWaves.Length)       // if we've gone through the whole current wave, and there's another wave ahead...
        {
            waveCounter++;          // advance to the next wave
            currentWave = enemyWaves[waveCounter];      // reassign current wave
        }
        else if (waveCounter + 1 >= enemyWaves.Length)       // if we have spawned everything...
        {
            print("Done Spawning");
            doneSpawning = true;
        }
    }
}
