using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float regen;
    public bool regenerating;

    private SpriteRenderer sprite;
    private SpriteRenderer outline;
    private CollisionControl collisionControl;
    private Health health;

    IEnumerator regenerate ()
    {
        regen = 0f;
        regenerating = true;

        while (regen < health.maxHealth)
        {
            yield return null;

            regen += (10f / 5f) * Time.deltaTime;
        }

        health.health = health.maxHealth;
        gameObject.layer = LayerMask.NameToLayer("Platform");

        outline.sprite = GameplayComponents.main.platformOutlineSprite;

        sprite.color = new Color(1f, 1f, 1f, 1f);
        outline.color = new Color(1f, 1f, 1f, 0f);

        regenerating = false;
    }

    private void Awake()
    {
        regen = 0f;
        regenerating = false;

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
        if (!health.dead) {
            if (collisionControl.collisionEnter && Tools.ExistsTag(collisionControl.collisionEnterCol, "Enemy"))
            {
                foreach (GameObject hit in collisionControl.collisionEnterCol) {
                    if (hit.tag == "Enemy") {
                        health.health -= hit.GetComponent<Enemy>().damage;
                    }
                }
            }

            if (health.health < health.maxHealth)
            {
                health.health += (10f / 3f) * Time.deltaTime;
            }

            sprite.color = new Color(1f, 1f, 1f, health.health / 10f);
            outline.color = new Color(1f, 1f, 1f, 10f - (health.health / 10f));
        }

        if (health.dead && !regenerating)
        {
            gameObject.layer = LayerMask.NameToLayer("Invincible");

            outline.sprite = GameplayComponents.main.platformInvincibleOutlineSprite;

            sprite.color = new Color(1f, 1f, 1f, 0f);
            outline.color = new Color(1f, 1f, 1f, 1f);

            StartCoroutine(regenerate());
        }
    }
}
