using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Experimental.U2D;

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

    public List<LevelData> levelsData = new List<LevelData>();

    public string currentLevelName;

    public int currentLevelNumber;
    public int currentWorld;


    /*[ContextMenu("Identify by Name")]
    public void identifyByName()
    {
        currentLevelNumber = levelsData.Find(level => level.name == currentLevelName).levelNumber;
        currentWorld = levelsData.Find(level => level.name == currentLevelName).world;
    }



    */
    [ContextMenu("Put Level")]
    public void resetLevel ()
    {
        /*if (levelsData.Exists(level => level.levelNumber == currentLevelNumber && level.world == currentWorld && level.name == currentLevelName))
        {
            levelsData.Find(level => level.levelNumber == currentLevelNumber && level.world == currentWorld).levelBoundsPoints = GameObject.Find("LevelBoundaries").GetComponent<PolygonCollider2D>().points;
            levelsData.Find(level => level.levelNumber == currentLevelNumber && level.world == currentWorld).enemyBoundsPoints = GameObject.Find("EnemyBoundaries").GetComponent<PolygonCollider2D>().points;
        } else
        {
            LevelData levelDataInstance = new LevelData();

            levelDataInstance.name = currentLevelName;
            for (int i = 0; i < SceneManager.sceneCount; i++) {
                if (SceneManager.GetSceneAt(i).name != "GameComponents")
                {
                    levelDataInstance.scene = SceneManager.GetSceneAt(i);
                }
            }
            levelDataInstance.levelNumber = currentLevelNumber;
            levelDataInstance.world = currentWorld;
            levelDataInstance.levelBoundsPoints = GameObject.Find("LevelBoundaries").GetComponent<PolygonCollider2D>().points;
            levelDataInstance.enemyBoundsPoints = GameObject.Find("EnemyBoundaries").GetComponent<PolygonCollider2D>().points;

            levelsData.Add(levelDataInstance);
        }*/
        Scene currentLevel = new Scene();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name != "GameComponents")
            {
                currentLevel = SceneManager.GetSceneAt(i);
            }
        }


        if (levelsData.Exists(level => level.scene == currentLevel))
        {
            levelsData.Find(level => level.scene == currentLevel).levelColour = GameObject.Find("LevelBoundaries").GetComponent<SpriteShapeRenderer>().color;
            levelsData.Find(level => level.scene == currentLevel).borderColour = Camera.main.backgroundColor;
            
            levelsData.Find(level => level.scene == currentLevel).levelBoundsPoints = GameObject.Find("LevelBoundaries").GetComponent<EdgeCollider2D>().points;
            levelsData.Find(level => level.scene == currentLevel).enemyBoundsPoints = GameObject.Find("EnemyBoundaries").GetComponent<EdgeCollider2D>().points;
        }
        else
        {
            LevelData levelDataInstance = new LevelData();

            levelDataInstance.name = currentLevelName;
            levelDataInstance.scene = currentLevel;
            levelDataInstance.levelNumber = currentLevelNumber;
            levelDataInstance.world = currentWorld;
            levelDataInstance.levelColour = GameObject.Find("LevelBoundaries").GetComponent<SpriteShapeRenderer>().color;
            levelDataInstance.borderColour = Camera.main.backgroundColor;
            levelDataInstance.levelBoundsPoints = GameObject.Find("LevelBoundaries").GetComponent<EdgeCollider2D>().points;
            levelDataInstance.enemyBoundsPoints = GameObject.Find("EnemyBoundaries").GetComponent<EdgeCollider2D>().points;

            levelsData.Add(levelDataInstance);
        }
    }


    public float cameraZoom;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public Sprite playerSprite;

    public void OnEnable()
    {
        main = this;
    }

    public void Awake()
    {
        main = this;
    }
}
