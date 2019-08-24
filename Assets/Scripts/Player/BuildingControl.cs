using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControl : MonoBehaviour
{
    private Vector3 lastRoundedPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameplayComponents.main.gridGhost.transform.position = new Vector3(Mathf.Round(GameplayComponents.main.player.transform.position.x / 3f) * 3f, Mathf.Round(GameplayComponents.main.player.transform.position.y / 3f) * 3f, 0f);
        GameplayComponents.main.gridGhost.transform.GetChild(0).transform.position = GameplayComponents.main.player.transform.position;

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
            Vector3 roundedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 hRound = new Vector3(Mathf.Round(roundedPosition.x / 3f) * 3f, Tools.RoundOffset(roundedPosition.y, 1.5f));
            Vector3 vRound = new Vector3(Tools.RoundOffset(roundedPosition.x, 1.5f), Mathf.Round(roundedPosition.y / 3f) * 3f);

            if (Vector3.Distance(roundedPosition, hRound) > Vector3.Distance(roundedPosition, vRound))
            {
                roundedPosition = vRound;
                GameObject.Find("PlatformGhost").transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            }
            else if (Vector3.Distance(roundedPosition, hRound) < Vector3.Distance(roundedPosition, vRound))
            {
                roundedPosition = hRound;
                GameObject.Find("PlatformGhost").transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }

            GameObject.Find("PlatformGhost").transform.position = roundedPosition;

            if (lastRoundedPosition != roundedPosition)
            {
                GameplayComponents.main.platformGhostZone.transform.position = roundedPosition;
                GameplayComponents.main.platformGhostZone.transform.rotation = GameplayComponents.main.platformGhost.transform.rotation;
                GameplayComponents.main.platformGhostZone.Play("Fade", 0);
            }

            lastRoundedPosition = roundedPosition;
        }

        if (Input.GetMouseButton(1) && GameplayComponents.main.platform.transform.position != GameplayComponents.main.platformGhost.transform.position)
        {
            GameObject.Find("Platform").transform.position = GameObject.Find("PlatformGhost").transform.position;
            GameObject.Find("Platform").transform.rotation = GameObject.Find("PlatformGhost").transform.rotation;
        }
    }
}
