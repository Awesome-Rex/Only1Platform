using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransportation : MonoBehaviour
{
    public Scene scene;

    public void loadScene ()
    {
        SceneManager.LoadScene(scene.name);
    }
}
