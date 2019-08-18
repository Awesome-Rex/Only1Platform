using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class KnockBackDeath : Enemy
{
    protected new Rigidbody2D rigidbody2D;
    protected CollisionControl collisionControl;
    protected List<SpriteRenderer> spriteRenderers;
    protected SpriteRenderer spriteRenderer;
    
    public sealed override void hit()
    {
        Debug.Log("I just died!");

        dead = true;
        gameObject.layer = LayerMask.NameToLayer("Unaffected");

        foreach (SpriteRenderer i in spriteRenderers) {
            i.material.SetFloat("_Flashing", 1f);
            i.material.SetColor("Colour", new Color(1f, 1f, 1f, 0.75f));
        }
        transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        Invoke("deadSprite", 0.1f);

        rigidbody2D.velocity = Vector3.zero;
        rigidbody2D.AddTorque(Random.Range(-5.0f, 5.0f));

        rigidbody2D.constraints = RigidbodyConstraints2D.None;

        if (Tools.ExistsTag(collisionControl.collisionEnterCol, "Platform"))
        {
            rigidbody2D.AddForce((transform.position - Tools.FindWithTag(collisionControl.collisionEnterCol, "Platform").transform.position).normalized * 50f);
        }
        else if (Tools.ExistsTag(collisionControl.collisionEnterCol, "Player") || GameplayComponents.main.player.playerShooting.aiming)
        {
            rigidbody2D.AddForce((transform.position - GameplayComponents.main.player.transform.position).normalized * 50f);
        }

        Destroy(gameObject, 2f);
    }

    private void deadSprite ()
    {
        foreach (SpriteRenderer i in spriteRenderers)
        {
            i.material.SetFloat("_Flashing", 0f);
            i.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
        transform.localScale = Vector3.one;
    }

    internal void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collisionControl = GetComponent<CollisionControl>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderers = new List<SpriteRenderer>();

        spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>().ToList());

    }

    // Start is called before the first frame update
    internal void Start()
    {
        
    }

    // Update is called once per frame
    internal void Update()
    {
        if (!dead)
        {
            if (collisionControl.collisionEnter && (Tools.ExistsTag(collisionControl.collisionEnterCol, "Platform") || Tools.ExistsTag(collisionControl.collisionEnterCol, "Player")))
            {
                hit();
            }
        }
    }
}
