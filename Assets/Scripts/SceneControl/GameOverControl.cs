using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = GameComponents.main.cameraZoom;

        Transform playerPieces = GameObject.Find("PlayerPieces").transform;

        //GameComponents gameComponents =

        playerPieces.rotation = GameComponents.main.playerRotation;
        playerPieces.position = GameComponents.main.playerPosition + -(playerPieces.right * ((GameComponents.main.playerSprite.pivot.x / GameComponents.main.playerSprite.rect.width) - 0.5f)) + -(playerPieces.up * ((GameComponents.main.playerSprite.pivot.y / GameComponents.main.playerSprite.rect.height) - 0.5f));

        playerPieces.GetComponent<PointEffector2D>().enabled = true;

        GameObject.Find("LeftWall").GetComponent<BoxCollider2D>().size = new Vector2(1, Camera.main.orthographicSize * 2f);
        GameObject.Find("RightWall").GetComponent<BoxCollider2D>().size = new Vector2(1, Camera.main.orthographicSize * 2f);
        GameObject.Find("BottomWall").GetComponent<BoxCollider2D>().size = new Vector2(1, (Camera.main.orthographicSize * Camera.main.aspect) * 2f);
        GameObject.Find("TopWall").GetComponent<BoxCollider2D>().size = new Vector2(1, (Camera.main.orthographicSize * Camera.main.aspect) * 2f);

        GameObject.Find("LeftWall").transform.position = new Vector3(-(Camera.main.orthographicSize * Camera.main.aspect), 0f, 0f);
        GameObject.Find("RightWall").transform.position = new Vector3((Camera.main.orthographicSize * Camera.main.aspect), 0f, 0f);
        GameObject.Find("BottomWall").transform.position = new Vector3(0f, -Camera.main.orthographicSize, 0f);
        GameObject.Find("TopWall").transform.position = new Vector3(0f, Camera.main.orthographicSize, 0f);

        foreach (Transform child in playerPieces.GetComponentsInChildren<Transform>())
        {
            child.SetParent(null);
        }

        Destroy(playerPieces.gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
