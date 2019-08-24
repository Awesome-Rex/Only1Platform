using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameplayComponents : MonoBehaviour
{
    public static GameplayComponents main;

    public PlayerControl player;

    public GameObject platform;
    public GameObject platformGhost;
    public Animator platformGhostZone;

    public GameObject gridGhost;

    public EdgeCollider2D levelBoundariesEdge;
    public PolygonCollider2D levelBoundariesFill;

    public PostProcessVolume postProcessing;

    public Sprite platformOutlineSprite;
    public Sprite platformInvincibleOutlineSprite;

    public PlaceHolder warning;
    public PlaceHolder blank;

    public Animator ammoIcon;

    private void Awake()
    {
        main = this;

        platformGhostZone.SetFloat("Length", 4f);
        ammoIcon.SetFloat("Length", 1f);
        
    }
}
