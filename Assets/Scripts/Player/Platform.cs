using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float regen;

    private SpriteRenderer sprite;
    private SpriteRenderer outline;
    private CollisionControl collisionControl;
    private Health health;

    IEnumerator regenerate ()
    {
        regen = 0f;

        while (regen < health.maxHealth)
        {
            yield return null;

            regen += (10f / 3f) * Time.deltaTime;
        }

        health.health = health.maxHealth;
        gameObject.layer = LayerMask.NameToLayer("Platform");

        sprite.color = new Color(1f, 1f, 1f, health.health / 10f);
        outline.color = new Color(1f, 1f, 1f, 10f - (health.health / 10f));
    }

    private void Awake()
    {
        regen = 0f;

        sprite = GetComponent<SpriteRenderer>();
        outline = transform.GetChild(0).GetComponent<SpriteRenderer>();
        collisionControl = GetComponent<CollisionControl>();
        health = GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionControl.collisionEnter && Tools.ExistsTag(collisionControl.collisionEnterCol, "Enemy"))
        {
            health.health -= Tools.FindWithTag(collisionControl.collisionEnterCol, "Enemy").GetComponent<Enemy>().damage;

            sprite.color = new Color(1f, 1f, 1f, health.health / 10f);
            outline.color = new Color(1f, 1f, 1f, 10f - (health.health / 10f));
        }

        if (health.health < health.maxHealth && !health.dead)
        {
            health.health += (10f / 2f);
        }

        if (health.dead)
        {
            gameObject.layer = LayerMask.NameToLayer("Invinsible");

            StartCoroutine(regenerate());
        }
    }
}
