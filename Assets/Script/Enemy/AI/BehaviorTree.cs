using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTree : MonoBehaviour
{

    private Node root = null;
    public Rigidbody2D rb = null;
    public Animator animator = null;   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        root = SetUpTree();
    }

    // Update is called once per frame
    void Update()
    {
        if (root != null) 
        { 
            root.EvaluateState();   
        }
    }

    protected abstract Node SetUpTree();
}
