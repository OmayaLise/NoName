using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Player_Data data;
    private PlayerControl controls;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controls = data.playerControl;
        controls.Enable();
        controls.Player.Attack.performed += ctx => QuickAttack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void QuickAttack()
    {
        animator.SetBool("isRunning", false);
    }
}
