using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : PhysicsOverride {

    #region Variables
    public float jumpTakeOffSpeed = 7f;
    public float jumpCancelModifier = 0.5f;

    //Character parameters
    public float percentage = 0.0f;
    private int doubleJumpLeft = 1;

    //Importing character components
    CharacterParameters character;
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
            doubleJumpLeft = character.doubleJump;
        }

        targetVelocity = move * character.moveSpeed;
    }
    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start () {
		
	}
	

	#endregion
}
