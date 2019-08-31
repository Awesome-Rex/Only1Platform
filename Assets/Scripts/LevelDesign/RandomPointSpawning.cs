using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPointSpawning : MonoBehaviour
{
    public List<SpawnRound> rounds;

    public float minFrequency;
    public float maxFrequency;

    IEnumerator spawnCycle()
    {
        while (true)
        {
            foreach (EnemySpawn i in rounds[Random.Range(0, rounds.Count)].enemies)
            {
                for (int j = 0; j < i.quantity; j++)
                {
                    //Spawnable enemyPrefab = Instantiate(i.enemy);

                    StartCoroutine(Spawnable.spawnEnemy(i.enemy));
                }
            }

            yield return new WaitForSeconds(Random.Range(minFrequency, maxFrequency + 0.01f));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnCycle());
    }

    private void Update()
    {

    }
}
