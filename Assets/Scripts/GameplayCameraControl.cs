using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCameraControl : MonoBehaviour
{
    private bool hitShaking;

    public IEnumerator hitShake (Vector3 a, Vector3 b)
    {
        hitShaking = true;

        startCoroutine(slowDown(0.5f, 0.01f, 0.05f));

        Vector3 origin = transform.position;

        for (float i = 0f; i < 0.25f; i += 0.05f)
        {
            transform.position = Vector3.Lerp(origin + a, origin + b, Random.Range(0.00f, 1.01f));

            yield return new WaitForSeconds(0.05f);
        }

        transform.position = origin;

        hitShaking = false;
    }

    public IEnumerator slowDown (float min, float intervals, float delay)
    {
        /*for (; Time.timeScale > min; Time.timeScale -= intervals)
        {
            yield return new WaitForSeconds(delay);
        }*/
        Time.timeScale = (0.5f);

        for (; Time.timeScale < 1; Time.timeScale += intervals)
        {
            yield return new WaitForSeconds(delay);
        }
    }

    void startCoroutine (IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    private void Awake()
    {
        hitShaking = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hitShaking && !GameplayComponents.main.player.damaged)
        {
            transform.position = Vector3.Lerp(transform.position, GameplayComponents.main.player.transform.position + new Vector3(0f, 0f, -10f), 0.25f);
        }
    }
}
