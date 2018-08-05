using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsOverride {

    #region Variables
    public float jumpTakeOffSpeed = 7f;
    public float jumpCancelModifier = 0.5f;

    //Character parameters
    public float percentage = 0.0f;
    public float weight = 5f;
    public float moveSpeed = 5f;
    public float jumpVelocity = 5f;
    public int doubleJump = 1;

    //Temporary variables
    private int doubleJumpLeft = 1;
    
    //Importing character components
    //
    #endregion

    #region Custom Methods
    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && doubleJumpLeft > 0)
        {
            velocity.y = jumpTakeOffSpeed;
            doubleJumpLeft -= 1;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if(velocity.y > 0)
            {
                velocity.y = velocity.y * jumpCancelModifier;
            }
        }

        if (grounded)
        {
            doubleJumpLeft = doubleJump;
        }

        targetVelocity = move * moveSpeed;
    }
    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start () {
		
	}
	

	#endregion
}
