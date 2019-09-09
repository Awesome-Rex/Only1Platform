using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameComponents : ScriptableObject
{
    public static GameComponents main;
    /*[SerializeField]
    public GameComponents gameComponents
    {
        set
        {
            main = value;
        }
    }*/

    public List<Level> LevelData = new List<Level>();


    public float cameraZoom;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public Sprite playerSprite;

    public void OnEnable()
    {
        main = this;
    }
}
