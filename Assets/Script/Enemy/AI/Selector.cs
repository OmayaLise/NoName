using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public Selector() :base() { }
    public Selector(List<Node> children) : base(children) { }
    public override NodeState EvaluateState()
    {
        bool anyChildRunning = false;

        foreach (Node item in children)
        {
            switch(item.EvaluateState())
            {
                case NodeState.FAILED:
                {
                    continue;
                }
                case NodeState.SUCESS:
                {
                    state = NodeState.SUCESS;
                    return state;
                }
                case NodeState.RUNNING:
                {
                    state = NodeState.RUNNING;
                    return state;
                }
                default:         
                {
                    continue;
                }
            }
        }

        state = NodeState.FAILED;
        return state;
    }
}
