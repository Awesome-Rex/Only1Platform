using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirection : SpawnSetting
{
    public sealed override void change(GameObject target)
    {
        target.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f)));
    }
}
