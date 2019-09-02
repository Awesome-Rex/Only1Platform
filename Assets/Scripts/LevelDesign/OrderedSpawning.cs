using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedSpawning : MonoBehaviour
{
    public bool looping = false;

    public List<SpawnRound> rounds;

    public int currentRound = 0;

    public bool hasIntroTransition = false;
    public TransitionDetect introTransition;

    // Start is called before the first frame update
    void Start()
    {
        if (!hasIntroTransition) {
            foreach (EnemySpawn enemy in rounds[currentRound].enemies)
            {
                for (int i = 0; i < enemy.quantity; i++)
                {
                    StartCoroutine(Spawnable.spawnEnemy(enemy.enemy));
                }
            }

            rounds[currentRound].transition.startCheck();
            if (rounds[currentRound].transition.hasTransitioned)
            {
                currentRound++;
            }
        } else
        {
            introTransition.startCheck();
            if (introTransition.hasTransitioned)
            {
                hasIntroTransition = false;
                Start();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasIntroTransition) {
            if ((!looping && currentRound < rounds.Count - 1) || looping)
            {
                rounds[currentRound].transition.updateCheck();
                if (rounds[currentRound].transition.hasTransitioned)
                {
                    currentRound++;

                    if (currentRound > rounds.Count - 1) {
                        currentRound = 0;

                        foreach (SpawnRound round in rounds)
                        {
                            round.reset();
                        }
                    }

                    foreach (EnemySpawn enemy in rounds[currentRound].enemies)
                    {
                        for (int i = 0; i < enemy.quantity; i++)
                        {
                            StartCoroutine(Spawnable.spawnEnemy(enemy.enemy));
                        }
                    }

                    rounds[currentRound].transition.startCheck();
                }
            }
        } else
        {
            introTransition.updateCheck();
            if (introTransition.hasTransitioned)
            {
                hasIntroTransition = false;
                Start();
            }
        }
    }
}
