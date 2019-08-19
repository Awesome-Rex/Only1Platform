﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameplayComponents : MonoBehaviour
{
    public static GameplayComponents main;

    public PlayerControl player;

    public GameObject platform;
    public GameObject platformGhost;
    public Animation platformGhostZone;

    public GameObject gridGhost;

    public PostProcessVolume postProcessing;


    public Sprite platformOutlineSprite;
    public Sprite platformInvincibleOutlineSprite;

    private void Awake()
    {
        main = this;
    }
}
