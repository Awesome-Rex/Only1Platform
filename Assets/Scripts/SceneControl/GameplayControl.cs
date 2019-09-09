using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayControl : MonoBehaviour
{
    public static GameplayControl main;

    public bool paused;

    public int score;
    

    public void pauseGame ()
    {
        paused = true;
        GameplayComponents.main.pauseMenu.SetActive(true);
        //GameplayComponents.main.pauseMenu.SetActive(false);

        GameplayComponents.main.pauseButton.enabled = false;
        Time.timeScale = 0f;
    }

    public void continueGame()
    {
        paused = false;
        GameplayComponents.main.pauseMenu.SetActive(false);
        //GameplayComponents.main.pauseMenu.SetActive(true);

        GameplayComponents.main.pauseButton.enabled = true;
        Time.timeScale = 1f;
    }

    public void pauseSettings ()
    {
        GameplayComponents.main.pauseMenuDefault.SetActive(false);
        GameplayComponents.main.pauseMenuSettings.SetActive(true);
    }

    public void pauseSettingsExit ()
    {
        GameplayComponents.main.pauseMenuDefault.SetActive(true);
        GameplayComponents.main.pauseMenuSettings.SetActive(false);
    }

    public void restartLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void loadScene (string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !paused)
        {
            pauseGame();
        } else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && paused)
        {
            continueGame();
        }

        /*if (GameplayComponents.main.player.GetComponent<Health>().dead)
        {
            GameComponents.main.playerPosition = GameplayComponents.main.player.transform.position - (Camera.main.transform.position + new Vector3(0f, 0f, 10f));
            GameComponents.main.playerRotation = GameplayComponents.main.player.transform.rotation;
            GameComponents.main.playerSprite = GameplayComponents.main.player.GetComponentInChildren<SpriteRenderer>().sprite;

            SceneManager.LoadScene("GameOver");
        }*/
    }
}
