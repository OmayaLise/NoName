using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyTree : BehaviorTree
{
    public List<Transform> waypoints = new List<Transform>();   

    protected override Node SetUpTree()
    {
        Node root = new TaskPatrol(transform, waypoints, rb, animator);
        return root;
    }
}
