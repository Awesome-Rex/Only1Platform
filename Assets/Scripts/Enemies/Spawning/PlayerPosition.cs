using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerPosition : SpawnSetting
{
    public sealed override void change (GameObject target)
    {
        target.transform.position = GameplayComponents.main.player.transform.position;
    }
}
