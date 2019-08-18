using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayComponents : MonoBehaviour
{
    public static GameplayComponents main;

    public PlayerControl player;

    public GameObject platform;
    public GameObject platformGhost;
    public Animation platformGhostZone;

    public GameObject gridGhost;

    private void Awake()
    {
        main = this;
    }
}
