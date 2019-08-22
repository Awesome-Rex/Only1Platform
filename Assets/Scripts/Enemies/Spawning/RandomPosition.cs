using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RandomPosition : SpawnSetting
{
    public sealed override void change(GameObject target)
    {
        target.transform.position = Tools.RandomPolygonPosition(GameplayComponents.main.levelBoundariesFill.points);
    }
}
