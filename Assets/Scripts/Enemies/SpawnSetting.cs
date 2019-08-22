using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnSetting : MonoBehaviour
{
    public bool changeBefore = false;

    public abstract void change(GameObject target);

    /*private void Start()
    {
        if (!changeBefore)
        {
            change(gameObject);
        }
    }*/
}
