using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransitionType
{
    UntilCleared, UntilTime, UntilScore
}

public struct EnemySpawn
{
    public Spawnable enemy;
}

public class AdditiveSpawning : MonoBehaviour
{
    public EnemySpawn enemies;
    public List<TransitionType> transitions;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
