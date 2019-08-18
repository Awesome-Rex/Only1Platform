using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour {

    public bool collisionEnter;
    public List<GameObject> collisionEnterCol;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionEnter = true;
        collisionEnterCol.Add(collision.collider.gameObject);

        GameObject collisionHit = collision.collider.gameObject;

        StartCoroutine(offColl(
            () => {
                collisionEnter = false;
                
                if (collisionEnterCol.Exists(col => col.GetInstanceID() == collisionHit.GetInstanceID()/*collision.collider.gameObject.GetInstanceID()*/)) {
                    collisionEnterCol.RemoveAll(col => col.GetInstanceID() == collisionHit.GetInstanceID()/*collision.collider.gameObject.GetInstanceID()*/);
                }
                /*if (collisionEnterCol.Contains(collision.collider.gameObject))
                {
                    collisionEnterCol.Remove(collision.collider.gameObject);
                }*/
            }
        ));
    }

    public bool collisionStay;
    public List<GameObject> collisionStayCol;

    private void OnCollisionStay2D(Collision2D collision)
    {
        collisionStay = true;
        if (!collisionStayCol.Contains(collision.collider.gameObject)) {
            collisionStayCol.Add(collision.collider.gameObject);
        }
    }

    public bool collisionExit;
    public List<GameObject> collisionExitCol;
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collisionStay)
        {
            collisionStay = false;
        }
        if (collisionStayCol.Exists(col => col.GetInstanceID() == collision.collider.gameObject.GetInstanceID())) {
            collisionStayCol.RemoveAll(col => col.GetInstanceID() == collision.collider.gameObject.GetInstanceID());
        }
        /*if (collisionStayCol.Contains(collision.collider.gameObject))
        {
            collisionStayCol.Remove(collision.collider.gameObject);
        }*/

        collisionExit = true;
        collisionExitCol.Add(collision.collider.gameObject);

        GameObject collisionHit = collision.collider.gameObject;

        StartCoroutine(offColl(
            () => {
                collisionExit = false;

                if (collisionExitCol.Exists(col => col.GetInstanceID() == collisionHit.GetInstanceID()/*collision.collider.gameObject.GetInstanceID()*/)) {
                    collisionExitCol.RemoveAll(col => col.GetInstanceID() == collisionHit.GetInstanceID()/*collision.collider.gameObject.GetInstanceID()*/);
                }
                /*if (collisionExitCol.Contains(collision.collider.gameObject))
                {
                    collisionExitCol.Remove(collision.collider.gameObject);
                }*/
            }
        ));
    }

    public IEnumerator offColl (System.Action boolChanged)
    {
        yield return null;

        boolChanged();
    }

    private void Awake()
    {
        collisionEnterCol = new List<GameObject>();
        collisionStayCol = new List<GameObject>();
        collisionExitCol = new List<GameObject>();
    }

    private void Update()
    {
        collisionEnterCol.RemoveAll(col => col == null);
        collisionStayCol.RemoveAll(col => col == null);
        collisionExitCol.RemoveAll(col => col == null);

    }
}
