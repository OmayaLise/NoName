using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Player_Data data;
    private PlayerControl controls;

    [SerializeField] float offsetColliderLeft;
    [SerializeField] float offsetColliderRight;

    private Rigidbody2D rb;
    private Animator animator;

    bool canDash = true;

    private Vector2 lastFacingDirection = Vector2.right;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        controls = data.playerControl;
        controls.Enable();
        controls.Player.Jump.performed += ctx => StartJump();
        controls.Player.Jump.canceled += ctx => EndJump();
        controls.Player.Movement.performed += ctx => StartRun();
        controls.Player.Movement.canceled += ctx => StopRun();
        controls.Player.Dash.performed += ctx => StartDash();

        if (data == null) 
        {
            Debug.Log(" No player data linked to Player Movement");
        }

        if (animator == null)
        {
            Debug.Log(" No animator linked to Player Movement");
        }

        if (controls == null)
        {
            Debug.Log(" No controls linked to Player Movement");
        }
    }

    private void Update()
    {
        if (!controls.Player.Dash.IsPressed())
        {
            float horizontalInput = controls.Player.Movement.ReadValue<Vector2>().x;
            rb.velocity = new Vector2(controls.Player.Movement.ReadValue<Vector2>().x * data.playerSpeed, rb.velocity.y);
        }

    }

    void StartJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, data.playerJumpStrenght);
        animator.SetBool("isJumping", true);
    }

    void EndJump()
    {
        animator.SetBool("isJumping", false);
    }

    void StartRun()
    {
        rb.velocity = new Vector2(controls.Player.Movement.ReadValue<Vector2>().x * data.playerSpeed, rb.velocity.y);
        if (rb.velocity.x < 0f)
        {
            animator.SetBool("isRunning", true);
            UpdateFacingDirection(Vector2.right);
        }
        else if (rb.velocity.x > 0f)
        {
            animator.SetBool("isRunning", true);

            UpdateFacingDirection(Vector2.left);

        }
    }

    void StopRun()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        animator.SetBool("isRunning", false);
    }

    void StartDash()
    {
        if (canDash)
        {
            animator.SetBool("isDashing", true);

            Vector2 dashDirection;
            if (GetComponent<SpriteRenderer>().flipX == true)
                dashDirection = Vector2.left;
            else
                dashDirection = Vector2.right;

            rb.AddForce(dashDirection * data.playerDashStrenght, ForceMode2D.Impulse);

            StartCoroutine(DashCooldown());
        }
    }

    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(1f);
        canDash = true;
    }

    private void UpdateFacingDirection(Vector2 direction)
    {
        // Check if there's a significant change in the horizontal direction
        if (Mathf.Abs(direction.x) > Mathf.Epsilon)
        {
            if ((direction.x < 0 && lastFacingDirection.x > 0) || (direction.x > 0 && lastFacingDirection.x < 0))
            {
                // Flip the sprite by multiplying the x local scale by -1
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;

                // Update the last facing direction
                lastFacingDirection = direction;
            }
        }
    }
}
