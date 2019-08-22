using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BorderPosition : SpawnSetting
{
    public sealed override void change(GameObject target)
    {
        target.transform.position = Tools.RandomEdgePosition(GameplayComponents.main.levelBoundariesEdge.points);
    }
}
