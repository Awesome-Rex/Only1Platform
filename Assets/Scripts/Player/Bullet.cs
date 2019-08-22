using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float distance;

    private bool dead;
    private float originDir;
    private Vector3 origin;
    private Vector3 destination;

    private new Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private CollisionControl collisionControl;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        
        trailRenderer = transform.GetChild(0).GetComponent<TrailRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collisionControl = GetComponent<CollisionControl>();

        rigidbody2D.velocity = transform.right * 50f;

        dead = false;
        originDir = transform.rotation.eulerAngles.z;
        origin = transform.position;
        destination = transform.position + (transform.right * distance);

    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && Vector3.Distance(origin, transform.position) > distance)
        {
            Destroy(gameObject);
        }

        if (!dead) {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f - (Vector3.Distance(origin, transform.position) / distance));
            trailRenderer.colorGradient.alphaKeys[1].alpha = 1f - (Vector3.Distance(origin, transform.position) / distance);
            trailRenderer.colorGradient.alphaKeys[0].alpha = (1f - (Vector3.Distance(origin, transform.position) / distance)) * 0.5f;
        }

        if (collisionControl.collisionEnter && collisionControl.collisionEnterCol.Count >= 1)
        {
            //Debug.Log(collisionControl.collisionEnterCol[1].collider.gameObject.name);
            foreach (GameObject i in collisionControl.collisionEnterCol) {
                Debug.Log(i.name);
            }
        }

        if (!dead && collisionControl.collisionEnter && (Tools.ExistsTag(collisionControl.collisionEnterCol, "Enemy") || Tools.ExistsTag(collisionControl.collisionEnterCol, "Platform") || Tools.ExistsTag(collisionControl.collisionEnterCol, "LevelBoundaries")))
        {
            //Debug.Log("Bullet hit");

            dead = true;

            gameObject.layer = LayerMask.NameToLayer("Unaffected");

            Vector3 newDir = new Vector3(0f, 0f, (originDir + 180f) + Random.Range(-22.5f, 22.5f));

            transform.rotation = Quaternion.Euler(newDir);
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.25f);

            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce((Quaternion.Euler(newDir) * Vector3.right) * 25f);
            rigidbody2D.angularVelocity = 0f;

            Destroy(trailRenderer.gameObject);
            Destroy(gameObject, 2f);
        }
    }
}
