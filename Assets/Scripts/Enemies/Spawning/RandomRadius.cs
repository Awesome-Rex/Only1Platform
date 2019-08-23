using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRadius : SpawnSetting
{
    public float radius;

    public sealed override void change(GameObject target)
    {
        target.transform.localPosition = (Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f))) * Vector3.right).normalized * Random.Range(0f, radius);
    }
}
