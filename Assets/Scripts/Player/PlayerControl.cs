using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerControl : MonoBehaviour
{
    public bool jumping;
    public float jumpStartTime;
    public float jumpLocationY;
    public float lastJumpFrameTime;
    public bool canJumpHold;

    public bool landable;

    public bool wallGrabbing;
    public Vector3 wallGrabPlatform;
    public bool canWallGrab;

    public bool wallJumpRestrict;
    public Tools.HDirection pastWallGrabDirection;

    public bool damaged;



    public List<Coroutine> wallGrabLimits;

    private Transform graphics;
    private CollisionControl collisionControl;
    private new Rigidbody2D rigidbody2D;
    private Animator animator;
    public PlayerShooting playerShooting;

    void allowMovement ()
    {
        wallJumpRestrict = false;

        if (!wallGrabbing)
        {
            rigidbody2D.gravityScale = 1f;
        }
        //canWallGrab = true;
    }

    IEnumerator allowWallGrabs (Tools.HDirection wallDir) // allows wall grabs after jumping off wall
    {
        Debug.Log("allow wall grabs");

        RaycastHit2D wallDetect = new RaycastHit2D();
        
        do {
            yield return null;

            Physics2D.queriesHitTriggers = false;
            if (wallDir == Tools.HDirection.Left)
            {
                wallDetect = Physics2D.BoxCast(transform.position + new Vector3(-0.5f - ((1f / 12f) / 2f), 0f, 0f), new Vector2((16f / 192f) - (4f / 192f), 1f - (4f / 192f)), 0f, Vector2.zero, 0f, ((1 << LayerMask.NameToLayer("Platform")) | (1 << LayerMask.NameToLayer("Invincible"))));
            } else if (wallDir == Tools.HDirection.Right)
            {
                wallDetect = Physics2D.BoxCast(transform.position + new Vector3(0.5f + ((1f / 12f) / 2f), 0f, 0f), new Vector2((16f / 192f) - (4f / 192f), 1f - (4f / 192f)), 0f, Vector2.zero, 0f, ((1 << LayerMask.NameToLayer("Platform")) | (1 << LayerMask.NameToLayer("Invincible"))));
            }
            Physics2D.queriesHitTriggers = true;
        } while (wallDetect.collider != null);
        

        canWallGrab = true;
    }

    IEnumerator stopWallGrabbing (Tools.HDirection wallDir)
    {
        pastWallGrabDirection = wallDir;
        canWallGrab = true;

        yield return new WaitForSeconds(2f);


        wallJumpRestrict = true;
        canWallGrab = false;
        Tools.startCoroutine(allowWallGrabs(wallDir), this);


        if (wallDir == Tools.HDirection.Left)
        {
            rigidbody2D.velocity = new Vector2(2.5f, 0);
        }
        else if (wallDir == Tools.HDirection.Right)
        {
            rigidbody2D.velocity = new Vector2(-2.5f, 0);
        }

        Invoke("allowMovement", 1f);
    }

    private void Awake()
    {
        jumping = false;
        lastJumpFrameTime = 0f;
        jumpLocationY = 0f;
        canJumpHold = true;

        landable = false;
        wallGrabbing = false;
        wallGrabPlatform = new Vector3();
        canWallGrab = true;
        wallJumpRestrict = false; /* stops movement after jumping off wall */
        damaged = false;
        playerShooting = GetComponent<PlayerShooting>();

        wallGrabLimits = new List<Coroutine>();

        graphics = transform.GetChild(0);
        collisionControl = GetComponent<CollisionControl>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesHitTriggers = false;
        RaycastHit2D platformDetect = Physics2D.BoxCast(transform.position + new Vector3(0f, -0.5f - ((1f / 12f) / 2f), 0f), new Vector2(1f - (4f / 192f), (16f / 192f) - (4f / 192f)), 0f, Vector2.zero, 0f, ((1 << LayerMask.NameToLayer("Platform")) | (1 << LayerMask.NameToLayer("Invincible"))));
        /*if (platformDetect.collider != null && platformDetect.collider.transform.rotation.eulerAngles.z != 0f)
        {
            platformDetect = new RaycastHit2D();
        }*/

        RaycastHit2D wallGrabDetectR = Physics2D.BoxCast(transform.position + new Vector3(0.5f + ((1f / 12f) / 2f), 0f, 0f), new Vector2((16f / 192f) - (4f / 192f), 1f - (4f / 192f)), 0f, Vector2.zero, 0f, ((1 << LayerMask.NameToLayer("Platform")) | (1 << LayerMask.NameToLayer("Invincible"))));
        RaycastHit2D wallGrabDetectL = Physics2D.BoxCast(transform.position + new Vector3(-0.5f - ((1f / 12f) / 2f), 0f, 0f), new Vector2((16f / 192f) - (4f / 192f), 1f - (4f / 192f)), 0f, Vector2.zero, 0f, ((1 << LayerMask.NameToLayer("Platform")) | (1 << LayerMask.NameToLayer("Invincible"))));
        Physics2D.queriesHitTriggers = true;

        if (Input.GetKey(KeyCode.A) && !wallJumpRestrict && !(wallGrabDetectL.collider/* != null && (wallGrabDetectL.collider.transform.rotation.eulerAngles.z != 0f)*/))
        {
            rigidbody2D.velocity = new Vector2(-5f, rigidbody2D.velocity.y);
            animator.SetTrigger("Move");
        }
        else if (Input.GetKey(KeyCode.D) && !wallJumpRestrict && !(wallGrabDetectR.collider != null/* && (wallGrabDetectR.collider.transform.rotation.eulerAngles.z != 0f)*/))
        {
            rigidbody2D.velocity = new Vector2(5f, rigidbody2D.velocity.y);
            animator.SetTrigger("Move");
        }
        else if (!wallJumpRestrict)
        {
            rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
        }


        if (wallGrabDetectR.collider != null && wallGrabDetectR.collider.transform.rotation.eulerAngles.z != 90f)
        {
            wallGrabDetectR = new RaycastHit2D();
        }
        if (wallGrabDetectL.collider != null && wallGrabDetectL.collider.transform.rotation.eulerAngles.z != 90f)
        {
            wallGrabDetectL = new RaycastHit2D();
        }


        if (jumping && !landable)
        {
            if (rigidbody2D.velocity.y < 0f || platformDetect.collider == null)
            {
                landable = true;
            }
        }

        if (wallGrabbing && (wallGrabDetectL.collider == null && wallGrabDetectR.collider == null))
        {
            StopCoroutine(wallGrabLimits[0]);
            wallGrabLimits.RemoveAt(0);

            jumping = true;
            wallGrabbing = false;

            rigidbody2D.gravityScale = 1f;

            graphics.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

            if (platformDetect.collider == null) {

                landable = false;

                rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                animator.SetTrigger("Fall");
                //animator.Play("HangTime", 0);
            } else if (platformDetect.collider != null)
            {
                landable = true;
            }
        }
        else if (platformDetect.collider == null && !jumping && !landable && !wallGrabbing)
        {
            jumping = true;
            landable = true;

            if ((wallGrabDetectL.collider == null && wallGrabDetectR.collider == null) || !canWallGrab)
            {
                rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

                animator.SetTrigger("Fall");

            } else if (wallGrabDetectL.collider != null || wallGrabDetectR.collider != null)
            {

            }
        } else if (jumping && (Input.GetKeyUp(KeyCode.W) || rigidbody2D.velocity.y < 0f))
        {
            canJumpHold = false;
        }


        if (!jumping && Input.GetKeyDown(KeyCode.W) && platformDetect.collider != null && platformDetect.collider.transform.rotation.eulerAngles.z == 0f)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Sqrt(-2.0f * -9.81f * 3f));

            jumping = true;
            jumpStartTime = Time.time;
            jumpLocationY = rigidbody2D.position.y;
            lastJumpFrameTime = Time.time;

            rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            animator.SetTrigger("Jump");
        } else if (jumping && Input.GetKey(KeyCode.W) && canJumpHold && Time.time - jumpStartTime < 1f)
        {
            //rigidbody2D.velocity += new Vector2(0f, Mathf.Sqrt(-2.0f * -9.81f * (4f * (Time.time - lastJumpFrameTime))));
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Sqrt(-2.0f * -9.81f * ((3f + (4f * (Time.time - jumpStartTime))) - (rigidbody2D.position.y - jumpLocationY))));

            lastJumpFrameTime = Time.time;
        }
        else if (!jumping && Input.GetKeyDown(KeyCode.W) && wallGrabbing)
        {
            StopCoroutine(wallGrabLimits[0]);
            wallGrabLimits.RemoveAt(0);
            Debug.Log(wallGrabLimits.Count);

            if (wallGrabDetectL.collider != null) {
                rigidbody2D.velocity = new Vector2(5f, Mathf.Sqrt(-2.0f * -9.81f * 7f));
            } else if (wallGrabDetectR.collider != null) {
                rigidbody2D.velocity = new Vector2(-5f, Mathf.Sqrt(-2.0f * -9.81f * 7f));
            }

            jumping = true;
            wallGrabbing = false;
            //canWallGrab = false;
            wallJumpRestrict = true;

            rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            Invoke("allowMovement", 0.5f);

            graphics.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, graphics.transform.rotation.eulerAngles.z));

            rigidbody2D.gravityScale = 1f;

            animator.SetTrigger("Jump");
        }
        else if (jumping && landable/* && (platformDetect.collider != null || ((wallGrabDetectR.collider != null || wallGrabDetectL.collider != null)&& !wallGrabbing))*/)
        {
            if (platformDetect.collider != null && platformDetect.collider.gameObject.tag != "LevelBoundaries")
            {
                jumping = false;
                canJumpHold = true;
                landable = false;

                rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;


                if (damaged)
                {
                    damaged = false;
                    GetComponentInChildren<Collider2D>().gameObject.layer = LayerMask.NameToLayer("Player");

                    ChromaticAberration chrom;
                    GameplayComponents.main.postProcessing.profile.TryGetSettings(out chrom);
                    chrom.intensity.value = 0.1f;
                }

                //canWallGrab = true;

                if (!playerShooting.canShoot) {
                    playerShooting.canShoot = true;
                }

                animator.SetTrigger("Land");
            }
            else if (canWallGrab && ((wallGrabDetectR.collider != null && wallGrabDetectR.collider.gameObject.tag != "LevelBoundaries") || (wallGrabDetectL.collider != null && wallGrabDetectL.collider.gameObject.tag != "LevelBoundaries")) && !wallGrabbing && rigidbody2D.velocity.y != 0f)
            {
                wallGrabbing = true;
                jumping = false;
                canJumpHold = true;
                landable = false;

                canWallGrab = true;
                wallJumpRestrict = false;

                rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;

                if (damaged)
                {
                    damaged = false;
                    GetComponentInChildren<Collider2D>().gameObject.layer = LayerMask.NameToLayer("Player");

                    ChromaticAberration chrom;
                    GameplayComponents.main.postProcessing.profile.TryGetSettings(out chrom);
                    chrom.intensity.value = 0.1f;
                }

                if (wallGrabDetectL.collider != null)
                {
                    animator.SetBool("HangingLeft", true);
                    graphics.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));

                    wallGrabLimits.Add(StartCoroutine(stopWallGrabbing(Tools.HDirection.Left)));
                } else if (wallGrabDetectR.collider != null)
                {
                    animator.SetBool("HangingLeft", false);
                    graphics.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

                    wallGrabLimits.Add(StartCoroutine(stopWallGrabbing(Tools.HDirection.Right)));
                }

                rigidbody2D.gravityScale = 0f;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);

                animator.SetTrigger("WallGrab");

                /*StopCoroutine(wallGrabLimits[0]);
                wallGrabLimits.RemoveAt(0);*/
            }
        }


        //animation enforcement
        if (!damaged) {
            if (jumping && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") || animator.GetCurrentAnimatorStateInfo(0).IsName("HangTime") || animator.GetCurrentAnimatorStateInfo(0).IsTag("PostHit")))
            {
                animator.Play("HangTime");
            } else if (wallGrabbing && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Hanging")))
            {
                //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Hanging"));
                //Debug.Break();
                animator.Play("Hanging");
            } else if (!jumping && !landable && !wallGrabbing && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Squash") || animator.GetCurrentAnimatorStateInfo(0).IsTag("PostHit")))
            {
                animator.Play("Idle");
            }
        } else if (damaged && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")))
        {
            animator.Play("Hit");
        }


        if (
            (!damaged && collisionControl.collisionEnter && (Tools.ExistsTag(collisionControl.collisionEnterCol, "Enemy") || Tools.ExistsTag(collisionControl.collisionEnterCol, "LevelBoundaries"))) || 
            (damaged && collisionControl.collisionEnter && Tools.ExistsTag(collisionControl.collisionEnterCol, "LevelBoundaries"))
        )
        {
            Debug.Log("Player got hit!");

            jumping = true;
            canJumpHold = false;
            landable = false;
            canWallGrab = true;

            ChromaticAberration chrom;
            GameplayComponents.main.postProcessing.profile.TryGetSettings(out chrom);
            chrom.intensity.value = 0.75f;

            rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            damaged = true;
            GetComponentInChildren<Collider2D>().gameObject.layer = LayerMask.NameToLayer("Invincible");

            animator.SetTrigger("Damage");

            if (Tools.ExistsTag(collisionControl.collisionEnterCol, "LevelBoundaries"))
            {
                StartCoroutine(Camera.main.GetComponent<GameplayCameraControl>().hitShake(
                    (rigidbody2D.velocity).normalized / 2f,
                    (rigidbody2D.velocity).normalized / -2f
                ));
            } else if (Tools.ExistsTag(collisionControl.collisionEnterCol, "Enemy")) {
                StartCoroutine(Camera.main.GetComponent<GameplayCameraControl>().hitShake(
                    (transform.position - Tools.FindWithTag(collisionControl.collisionEnterCol, "Enemy").transform.position).normalized / 2f,
                    (transform.position - Tools.FindWithTag(collisionControl.collisionEnterCol, "Enemy").transform.position).normalized / -2f
                ));
            }

            //if (!collisionControl.collisionEnterCol.Exists(hit => hit.name == "LevelBoundaries")) {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Sqrt(-2.0f * -9.81f * 4f));
            //}
            rigidbody2D.gravityScale = 1f;

        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("Square").transform.position = Tools.RandomPolygonPosition(GameObject.Find("LevelBoundaries").GetComponent<PolygonCollider2D>().points);
        }*/
    }
}
