using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public Sequence() :base() { }
    public Sequence(List<Node> children) : base(children) { }
    public override NodeState EvaluateState()
    {
        bool anyChildRunning = false;

        foreach (Node item in children)
        {
            switch(item.EvaluateState())
            {
                case NodeState.FAILED:
                {
                    state = NodeState.FAILED;
                    return state;
                }
                case NodeState.SUCESS:
                {
                    continue;
                }
                case NodeState.RUNNING:
                {
                    anyChildRunning = true;
                        continue;
                }
                default:         
                {
                    state = NodeState.RUNNING;
                    return state;
                }
            }
        }

        state = anyChildRunning ? NodeState.RUNNING : NodeState.SUCESS;
        return state;
    }
}
