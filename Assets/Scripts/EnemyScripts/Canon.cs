using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : KnockBackDeath
{
    private GameObject canonBall;


    IEnumerator pointNShoot ()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 5f));

            while (transform.right != (GameplayComponents.main.player.transform.position - transform.position).normalized)
            {
                /*transform.rotation = Quaternion.Euler(Vector3.RotateTowards(
                    transform.right,
                    (GameplayComponents.main.player.transform.position - transform.position),
                    5f * Time.deltaTime,
                    //Mathf.Abs(transform.rotation.eulerAngles.z - (Quaternion.LookRotation(GameplayComponents.main.player.transform.position - transform.position, Vector3.up).eulerAngles).z)) * (Mathf.PI / 180f)
                    0f

                ));*/

                //transform.rotation = Quaternion.Lerp(transform.rotation, (GameplayComponents.main.player.transform.position - transform.position). * Quaternion, 0.1f);

                if (dead)
                {
                    yield break;
                }

                float rotateDirection = Mathf.Sign(-Vector3.Cross(GameplayComponents.main.player.transform.position - transform.position, transform.right).z);

                transform.Rotate(new Vector3(0f, 0f, Mathf.Sign(-Vector3.Cross(GameplayComponents.main.player.transform.position - transform.position, transform.right).z) * 50f * Time.deltaTime));

                if (rotateDirection > 0f && transform.rotation.eulerAngles.z > Quaternion.LookRotation(Vector3.right, Vector3.forward).eulerAngles.z)
                {
                    transform.right = GameplayComponents.main.player.transform.position - transform.position;
                } else if (rotateDirection < 0f && transform.rotation.eulerAngles.z < Quaternion.LookRotation(Vector3.right, Vector3.forward).eulerAngles.z)
                {
                    transform.right = GameplayComponents.main.player.transform.position - transform.position;

                }

                yield return null;
            }

            GameObject canonBallPrefab = Instantiate(canonBall);
            canonBall.transform.position = transform.position;
            canonBall.transform.right = transform.right;
        }
    }

    private new void Awake()
    {
        base.Awake();

        canonBall = Resources.Load("Prefabs/Enemies/CanonBall") as GameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        //transform.right = GameplayComponents.main.player.transform.position - transform.position;
        StartCoroutine(pointNShoot());
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
