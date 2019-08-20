using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : KnockBackDeath
{
    public float distance;
    public float speed;

    private Vector3 origin;

    new private void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    internal void Start()
    {

        origin = transform.position;
        dead = false;
        rigidbody2D.velocity = (transform.right * speed);
    }

    // Update is called once per frame
    new internal void Update()
    {
        base.Update();

        if (!dead)
        {
            if (Vector3.Distance(transform.position, origin) >= distance)
            {
                Destroy(gameObject);
            }

            spriteRenderer.color = new Color(1f, 1f, 1f, Vector3.Distance(transform.position, origin) > distance * (3f / 4f) ? 1f - (Vector3.Distance(transform.position, origin) - (distance * (3f / 4f))) / (distance / 4f) : 1f);
        }
    }
}
