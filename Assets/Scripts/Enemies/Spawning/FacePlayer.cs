using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : SpawnSetting
{
    public sealed override void change(GameObject target)
    {
        target.transform.right = GameplayComponents.main.player.transform.position - transform.position;
    }
}
