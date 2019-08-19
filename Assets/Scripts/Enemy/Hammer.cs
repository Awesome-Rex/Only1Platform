using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Missile
{
    float direction;

    // Start is called before the first frame update
    new void Start()
    {
        ((Missile)this).Start();
        direction = Mathf.Sign(Random.Range(-1.0f, 1.0f)) * 360f;
    }

    // Update is called once per frame
    new void Update()
    {
        ((Missile)this).Update();
        rigidbody2D.rotation += (direction * Time.deltaTime);
    }
}
