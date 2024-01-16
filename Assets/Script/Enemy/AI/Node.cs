using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    RUNNING,
    SUCESS,
    FAILED
}
public class Node
{
    protected NodeState state;

    public Node parent;
    protected List<Node> children;   
    private Dictionary<string, object> dataContext = new Dictionary<string, object>();
    // Update is called once per frame
   public Node()
    {
        parent = null;
    }

    public Node(List<Node> childrenGiven)
    {
        foreach (Node item in childrenGiven)
        {
            AttachToParent(item);
        }
    }

    private void AttachToParent(Node node) 
    { 
        node.parent = this; 
        children.Add(node); 
    }

    public virtual NodeState EvaluateState() => NodeState.FAILED;

    public void SetData(string key, object value)
    {
        dataContext[key] = value;
    }

    public object GetData(string key) 
    {
        object value = null;
        if(dataContext.TryGetValue(key, out value))
        {
            return value;
        }

        Node node = parent;
        while (node != null) 
        { 
            value = node.GetData(key);
            if (value != null)
            {
                return value;
            }

            node = node.parent; 
        }

        return null;   
    }

    public bool ClearData(string key)
    {
        if (dataContext.ContainsKey(key))
        {
            dataContext.Remove(key);
            return true;
        }

        Node node = parent;
        while (node != null)
        {
            bool cleared = node.ClearData(key);
            if(cleared) 
            { 
                return true;    
            }
            node = node.parent;
        }
        return false;
    }
}
