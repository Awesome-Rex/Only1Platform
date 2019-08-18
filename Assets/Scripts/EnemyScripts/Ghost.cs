using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : KnockBackDeath
{
    private bool canTeleport;

    private Animator animator;
    private CircleCollider2D circleCollider2D;

    IEnumerator cycle ()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));

            if (dead)
            {
                yield break;
            }

            animator.SetTrigger("StartFade");
            canTeleport = false;

            while (!canTeleport)
            {
                yield return null;
            }
        }
    }

    public void teleport ()
    {
        circleCollider2D.enabled = false;

        transform.position = GameplayComponents.main.player.transform.position + (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0f)).normalized * Tools.ReturnIfZero(Vector3.Distance(transform.position, GameplayComponents.main.player.transform.position) - 3f);

        Invoke("reAppear", Random.Range(0.5f, 1.0f));
    }

    private void reAppear ()
    {
        animator.SetTrigger("Re-Appear");

        canTeleport = true;
    }

    public void allowCollision ()
    {
        circleCollider2D.enabled = true;
    }

    new void Awake()
    {
        ((KnockBackDeath)this).Awake();

        canTeleport = true;

        animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Start is called before the first frame update
    new void Start()
    {
        StartCoroutine(cycle());   
    }

    // Update is called once per frame
    new void Update()
    {
        ((KnockBackDeath)this).Update();
    }
}
