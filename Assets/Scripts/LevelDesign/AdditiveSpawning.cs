using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    public bool hasTransitioned;

    public virtual void startCheck() { }
    public virtual void updateCheck() { }

    public Transition ()
    {
        hasTransitioned = false;
    }
}

[System.Serializable]
public class ClearTransition : Transition
{
    public override void startCheck()
    {
        
    }

    public override void updateCheck()
    {
        if (GameplayComponents.main.enemyHolder.transform.childCount == 0)
        {
            hasTransitioned = true;
        }
    }
}

[System.Serializable]
public class TimeTransition : Transition
{
    public int wait;

    public override void startCheck()
    {
        Tools.CustomInvoke(() => hasTransitioned = true, wait);
    }

    public override void updateCheck()
    {

    }
}

[System.Serializable]
public class ScoreTransition : Transition
{
    public int scoreAmount;

    private int startScore;

    public override void startCheck()
    {
        startScore = GameplayControl.main.score;
    }

    public override void updateCheck()
    {
        if (GameplayControl.main.score - startScore >= scoreAmount)
        {
            hasTransitioned = true;
        }
    }
}

[System.Serializable]
public struct SpawnRound
{
    public List<EnemySpawn> enemies;

    public Transition transition;
}

[System.Serializable]
public class EnemySpawn
{
    public Spawnable enemy;
    public int quantity;

    public EnemySpawn ()
    {
        quantity = 1;
    }
}

public class AdditiveSpawning : MonoBehaviour
{
    public List<SpawnRound> rounds;

    public int currentRound = 0;

    public float minFrequency;
    public float maxFrequency;

    IEnumerator spawnCycle ()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minFrequency, maxFrequency + 0.01f));

            /*float maxWeight = 0;

            for (int i = currentRound; i < rounds.Count; i++)
            {
                foreach (EnemySpawn j in rounds[i].enemies)
                {
                    maxWeight += j.quantity;
                }
            }

            float chosenEnemy*/

            foreach (EnemySpawn i in rounds[Random.Range(currentRound, rounds.Count)].enemies)
            {
                for (int j = 0; j < i.quantity; j++)
                {
                    Spawnable.spawnEnemy(i.enemy);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnCycle());
    }

    private void Update()
    {
        if (currentRound < rounds.Count)
        {
            rounds[currentRound].transition.updateCheck();
            if (rounds[currentRound].transition.hasTransitioned)
            {
                currentRound++;
            }
        }
    }
}
