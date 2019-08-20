using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tracker : KnockBackDeath
{
    new void Awake()
    {
        ((KnockBackDeath)this).Awake();
    }

    // Start is called before the first frame update
    new void Start()
    {
        
    }

    // Update is called once per frame
    new void Update()
    {
        ((KnockBackDeath)this).Update();

        if (!dead)
        {
            RaycastHit2D movementDetect = new RaycastHit2D();

            List<float> movableLocations = new List<float>();

            for (float i = 0; i < 360; i += 45)
            {
                movementDetect = Physics2D.Raycast(transform.position, Quaternion.Euler(new Vector3(0f, 0f, i)) * Vector3.right, 5f, 1 << LayerMask.NameToLayer("Platform"));

                if (movementDetect.collider == null)
                {
                    movableLocations.Add(i);
                }
            }
            
            movableLocations = movableLocations.OrderBy(dir => Vector3.Distance(transform.position + (Quaternion.Euler(new Vector3(0f, 0f, dir)) * Vector3.right), GameplayComponents.main.player.transform.position)).ToList();
            movableLocations.RemoveRange(2, movableLocations.Count - 2);


            for (Quaternion i = Quaternion.Euler(new Vector3(0f, 0f, movableLocations[0])); i != Quaternion.Euler(new Vector3(0f, 0f, movableLocations[1])); i = Quaternion.RotateTowards(i, Quaternion.Euler(new Vector3(0f, 0f, movableLocations[1])), 11.25f))
            {
                if (i != Quaternion.Euler(new Vector3(0f, 0f, movableLocations[0]))) {
                    movementDetect = Physics2D.Raycast(transform.position, i * Vector3.right, 5f, 1 << LayerMask.NameToLayer("Platform"));

                    if (movementDetect.collider == null)
                    {
                        movableLocations.Add(i.eulerAngles.z);
                    }
                }

                //Debug.Break();
            }

            movableLocations = movableLocations.OrderBy(dir => Vector3.Distance(transform.position + (Quaternion.Euler(new Vector3(0f, 0f, dir)) * Vector3.right), GameplayComponents.main.player.transform.position)).ToList();

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, movableLocations[0])), 360f * Time.deltaTime);
            rigidbody2D.velocity = transform.right * 5f;

        }
    }
}
