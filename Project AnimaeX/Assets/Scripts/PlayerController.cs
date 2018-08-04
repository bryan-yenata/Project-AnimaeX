using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    public PlaceholderCharacter character;

    //Importing character components
    //
    #endregion

    #region Custom Methods
    private void UpdateMovement()
    {
        //character.animator.SetBool("Walking", false); //Resets walking animation to idle

        if (!character.canMove)
        { //Return if player can't move
            return;
        }
        else
        {
            Move();
        }
        
    }


    private void Move()
    {
        if (character.doubleJump > 0 && Input.GetKeyDown(KeyCode.Space))
        { //Drop bomb
            Jump();
        }

        if (Input.GetKey(KeyCode.W))
        {
        }

        if (Input.GetKey(KeyCode.A))
        { //Left movement
            character.rigidBody.velocity = new Vector3(-character.moveSpeed, character.rigidBody.velocity.y, 0);
            character.myTransform.rotation = Quaternion.Euler(0, 270, 0);
            //character.animator.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.S))
        { //Crouch
        }

        if (Input.GetKey(KeyCode.D))
        { //Right movement
            character.rigidBody.velocity = new Vector3(character.moveSpeed, character.rigidBody.velocity.y, 0);
            character.myTransform.rotation = Quaternion.Euler(0, 90, 0);
            //character.animator.SetBool("Walking", true);
        }

        
    }
    

    void Jump()
    {
        character.rigidBody.velocity = Vector3.up * character.jumpVelocity;
    }
    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();

    }

	#endregion
}
