using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public enum Grade
{
    Fm, F, Fp, Dm, D, Dp, Cm, C, Cp, Bm, B, Bp, Am, A, Ap
}

public class Game
{
    public static Game main;

    public List<Level> levels;

    public Level currentLevel;

    public Settings settings;

    public Game () {
        main = this;

        levels = new List<Level>();
        
        settings = new Settings();
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/GameData.game"); //you can call it anything you want
        bf.Serialize(file, Game.main);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameData.game", FileMode.Open);
            Game.main = (Game)bf.Deserialize(file);
            file.Close();
        }
    }
}

public class Settings
{
    public float SFXVolume;
    public float musicVolume;

    public bool controlUI;

    public Settings() {
        SFXVolume = 1f;
        musicVolume = 1f;

        controlUI = true;


    }
}

public class SceneTransitionData
{
    public float cameraZoom;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public Sprite playerSprite;
}

public class Level
{
    public Dictionary<Grade, string> gradeToText = new Dictionary<Grade, string>
    {
        [Grade.Fm] = "F-",
        [Grade.F] = "F",
        [Grade.Fp] = "F+",
        [Grade.Dm] = "D-",
        [Grade.D] = "D",
        [Grade.Dp] = "D+",
        [Grade.Cm] = "C-",
        [Grade.C] = "C",
        [Grade.Cp] = "C+",
        [Grade.Bm] = "B-",
        [Grade.B] = "B",
        [Grade.Bp] = "B+",
        [Grade.Am] = "A-",
        [Grade.A] = "A",
        [Grade.Ap] = "A+"
    };

    public int highScore;
    public float time;

    public enum LevelState { Passed, Unlocked, Locked }
    public LevelState levelState;

    public LevelData levelData;

}

[System.Serializable]
public class LevelData
{
    public string name;
    public Scene scene;

    public int world;
    public int levelNumber;

    public Color levelColour;
    public Color borderColour;

    public Vector2[] levelBoundsPoints;
    public Vector2[] enemyBoundsPoints;
}

