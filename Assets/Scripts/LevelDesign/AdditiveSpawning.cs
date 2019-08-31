using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    public bool enabled = false;

    [SerializeField] public string _transitionType;
    [SerializeField] public string transitionType
    {
        get
        {
            return _transitionType;
        }
        set
        {
            _transitionType = value;

            Transition.reAssign(this, _transitionType);
        }
    }

    private static void reAssign (Transition transition, string type)
    {
        switch (type)
        {
            case "clear":
                transition = new ClearTransition();
                break;
            case "time":
                transition = new TimeTransition();
                break;
            case "score":
                transition = new ScoreTransition();
                break;
        }
    }

    [SerializeField] public bool hasTransitioned;



    public virtual void startCheck() { }
    public virtual void updateCheck() { }

    public Transition ()
    {
        hasTransitioned = false;
    }
}

[System.Serializable]
public class TransitionDetect
{
    /*[SerializeField]
    public System.Tuple<TimeTransition, bool> time_Trans;
    public System.Tuple<ClearTransition, bool> clear_Trans;
    public System.Tuple<ScoreTransition, bool> score_Trans;*/

    public bool hasTransitioned = false;

    public TimeTransition time_Trans;
    public ClearTransition clear_Trans;
    public ClearTransition score_Trans;

    private Transition[] transitionsChecking
    {
        get
        {
            return new Transition[] { time_Trans, clear_Trans, score_Trans};
        }
    }

    public void startCheck()
    {
        foreach (Transition i in transitionsChecking)
        {
            if (i.enabled) {
                i.startCheck();
                if (i.hasTransitioned)
                {
                    hasTransitioned = true;
                    return;
                }
            }
        }
    }

    public void updateCheck()
    {
        foreach (Transition i in transitionsChecking)
        {
            if (i.enabled)
            {
                i.updateCheck();
                if (i.hasTransitioned)
                {
                    hasTransitioned = true;
                    return;
                }
            }
        }
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
        GameplayControl.main.StartCoroutine(Tools.CustomInvoke(() => hasTransitioned = true, wait));
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
public class SpawnRound
{
    public List<EnemySpawn> enemies;
    
    public TransitionDetect transition;

    /*public void changeTransition (Transition type)
    {
        transitions[transitions.Count - 1] = type;
    }*/
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
                    StartCoroutine(Spawnable.spawnEnemy(i.enemy));
                }
            }

            yield return new WaitForSeconds(Random.Range(minFrequency, maxFrequency + 0.01f));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rounds[currentRound].transition.startCheck();
        if (rounds[currentRound].transition.hasTransitioned)
        {
            currentRound++;
        }

        StartCoroutine(spawnCycle());
    }

    private void Update()
    {
        if (currentRound < rounds.Count)
        {
            /*foreach (Transition transition in rounds[currentRound].transitions) {
                transition.updateCheck();
                if (transition.hasTransitioned)
                {
                    currentRound++;
                }
            }*/
            rounds[currentRound].transition.updateCheck();
            if (rounds[currentRound].transition.hasTransitioned)
            {
                currentRound++;

                rounds[currentRound].transition.startCheck();
            }
        }
    }
}
