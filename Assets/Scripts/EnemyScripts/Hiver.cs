using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiver : KnockBackDeath
{
    Vector3 targetOffset;

    new void Awake()
    {
        ((KnockBackDeath)this).Awake();
        targetOffset = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0f);
    }

    // Update is called once per frame
    new void Update()
    {
        ((KnockBackDeath)this).Update();

        if (!dead) {
            transform.right = (GameplayComponents.main.player.transform.position + targetOffset) - transform.position;
            rigidbody2D.velocity += (Vector2)(((GameplayComponents.main.player.transform.position + targetOffset) - transform.position).normalized * 5f * Time.deltaTime);
            if (rigidbody2D.velocity.magnitude < 7.5f) {

            } else
            {
                rigidbody2D.velocity = (rigidbody2D.velocity.normalized) * 7.5f;
            }
        }
    }
}
