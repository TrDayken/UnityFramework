using Framework.AMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : Controller<GamePlayApplication>
{
    private PlayerModel playerModel;
    private PlayerView playerView;
    private WalkState walkState;

    // Start is called before the first frame update
    void Start()
    {
        playerModel = app.model.GetComponentInChildren<PlayerModel>();
        playerView = app.view.GetComponentInChildren<PlayerView>();

        Debug.Log(playerView != null);
        Debug.Log(playerView.animator != null);

        walkState = playerView.animator.GetBehaviour<WalkState>();
        walkState.SetModel(playerModel);
        walkState.SetView(playerView);
    }

    public void Move()
    {
        //float moveBy = inputDirection * playerModel.moveSpeed;
        //playerView.rb.velocity = new Vector2(moveBy, playerView.rb.velocity.y);
    }

    private void Jump()
    {
        playerModel.isGrounded = Physics2D.OverlapCircle(playerModel.feetPosition.position, playerModel.checkRadius, playerModel.whatIsGround);

        if (playerModel.isGrounded && Input.GetKeyDown(playerModel.jumpKey))
        {
            playerModel.isJumping = true;
            playerModel.ResetJumpTimeCounter(); 
            playerView.rb.velocity = Vector2.up * playerModel.jumpForce; 
        }

        if (Input.GetKey(playerModel.jumpKey) && playerModel.isJumping == true)
        {
            if(playerModel.jumpTimeCounter > 0)
            {
                playerView.rb.velocity = Vector2.up * playerModel.jumpForce;
                playerModel.jumpTimeCounter -= Time.deltaTime; 
            }
            else
            {
                playerModel.isJumping = false; 
            }
        }

        if(Input.GetKeyUp(playerModel.jumpKey))
        {
            playerModel.isJumping = false; 
        }
    }

    public void UpdateAnimator()
    {
        var inputDirection = Input.GetAxisRaw("Horizontal");
        playerView.animator.SetBool("isWalking", inputDirection != 0); 
    }

    public void UpdateLoop()
    {
        this.Jump(); 
    }
}
