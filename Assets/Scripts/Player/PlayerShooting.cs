using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerShooting : MonoBehaviour
{
    public bool canShoot;
    public bool aiming;

    private SpriteCone shootZone;
    private PlayerControl playerControl;
    private new Rigidbody2D rigidbody2D;
    private GameObject bullet;

    IEnumerator shootPrepare ()
    {
        canShoot = false;
        aiming = true;

        float startTime = Time.time;

        float angle = 0f;
        float distance = 0f;

        shootZone.gameObject.SetActive(true);

        while (Time.time - startTime < 1f && Input.GetMouseButton(0))
        {
            if (playerControl.damaged)
            {
                shootZone.gameObject.SetActive(false);
                aiming = false;

                yield break;
            }

            if (Time.time - startTime > 1f)
            {
                break;
            }

            yield return null;

            angle = 22.5f + ((Time.time - startTime) * 22.5f);
            distance = 3f + ((Time.time - startTime) * 2f);

            shootZone.radius = distance;
            shootZone.transform.GetChild(0).localScale = new Vector3(distance * 2f, distance * 2f, 1f);

            shootZone.degrees = angle;

            shootZone.transform.right = (Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0f, 10f)) - shootZone.transform.position;
        }

        List<RaycastHit2D> detectedEnemies = new List<RaycastHit2D>();

        RaycastHit2D enemyDetect = new RaycastHit2D();

        for (float i = -(angle / 2f); i < (angle / 2f) + 5f; i += 5f)
        {
            Physics2D.queriesHitTriggers = false;
            if (i <= angle / 2f) {
                enemyDetect = Physics2D.Raycast(transform.position, (shootZone.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, shootZone.transform.rotation.eulerAngles.z + i))) * Vector3.right, distance, (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Platform")));
                
            } else if (i > angle / 2f)
            {
                enemyDetect = Physics2D.Raycast(transform.position, (shootZone.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, shootZone.transform.rotation.eulerAngles.z + angle / 2f))) * Vector3.right, distance, (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Platform")));
            }
            Physics2D.queriesHitTriggers = true;

            if (enemyDetect.collider != null/* && !detectedEnemies.Contains(enemyDetect)*/ && enemyDetect.collider.gameObject.tag == "Enemy")
            {
                detectedEnemies.Add(enemyDetect);
            }
        }

        foreach(RaycastHit2D i in detectedEnemies)
        {
            if (!i.collider.GetComponent<Enemy>().dead) {
                i.collider.GetComponent<Enemy>().hit();
            }
        }

        for (float i = 0f; i < Mathf.Ceil(angle * 11.25f) / 11.25f; i += 11.25f) {
            GameObject bulletPrefab = Instantiate(bullet);
            bulletPrefab.transform.position = transform.position;
            bulletPrefab.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, shootZone.transform.rotation.eulerAngles.z + Random.Range(-(angle / 2f), (angle / 2f))));
            bulletPrefab.GetComponent<Bullet>().distance = distance/* * 1.5f*/;
        }

        rigidbody2D.gravityScale = 0f;
        //rigidbody2D.AddForce(-shootZone.transform.right * 150f);
        rigidbody2D.velocity = -shootZone.transform.right * 10f;

        playerControl.wallJumpRestrict = true;
        playerControl.Invoke("allowMovement", 0.1f);
        //Tools.CustomInvoke(() => GameplayComponents.main.player.playerShooting.rigidbody2D.gravityScale = 1f, 1f);

        shootZone.gameObject.SetActive(false);
        aiming = false;
    }

    private void Awake () {
        canShoot = true;
        aiming = false;

        shootZone = transform.GetChild(2).GetComponent<SpriteCone>();
        playerControl = GetComponent<PlayerControl>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        bullet = Resources.Load("Prefabs/Bullet") as GameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && !aiming && !playerControl.damaged)
        {
            StartCoroutine(shootPrepare());
        }
    }
}
