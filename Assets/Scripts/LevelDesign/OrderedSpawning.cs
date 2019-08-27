using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedSpawning : MonoBehaviour
{
    public List<SpawnRound> rounds;

    public int currentRound = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentRound < rounds.Count)
        {
            rounds[currentRound].transition.updateCheck();
            if (rounds[currentRound].transition.hasTransitioned)
            {
                currentRound++;

                foreach (EnemySpawn enemy in rounds[currentRound].enemies)
                {
                    for (int i = 0; i < enemy.quantity; i++)
                    {
                        Spawnable.spawnEnemy(enemy.enemy);
                    }
                }
            }
        }
    }
}
