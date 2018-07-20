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
    private void Move()
    {
        //animator.SetBool("Walking", false); //Resets walking animation to idle

        if (!character.canMove)
        { //Return if player can't move
            return;
        }

        //Move according to controls

        //Move left
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = character.lookLeft;
            character.moveDirection = transform.TransformDirection(-character.moveDirection);
            character.moveDirection = Vector3.left*character.moveSpeed;

            //character.animation.SetBool("IsRunning", true);
        }

        //Move right
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = character.lookRight;
            character.moveDirection = transform.TransformDirection(character.moveDirection);
            character.moveDirection = Vector3.right* character.moveSpeed;
            //character.animation.SetBool("IsRunning", true);
        }

        if (Input.GetButtonDown("Jump"))
        { //Drop bomb
            Jump();
        }
    }

    //    private void Move()
    //    {
    //        CharacterController controller = GetComponent<CharacterController>();
    //        if (controller.isGrounded)
    //        {

    //            anim.SetBool("IsRunning", false);

    //            moveDirection = new Vector3(-(Input.GetAxis("Vertical")), 0, Input.GetAxis("Horizontal"));

    //            if (Input.GetButton("Jump"))
    //                Jump();

    //            if (Input.GetKey(KeyCode.A))
    //            {

    //                transform.rotation = lookLeft;
    //                moveDirection = transform.TransformDirection(-moveDirection);
    //                moveDirection *= speed;

    //                anim.SetBool("IsRunning", true);

    //            }

    //            if (Input.GetKey(KeyCode.D))
    //            {
    //                transform.rotation = lookRight;
    //                moveDirection = transform.TransformDirection(moveDirection);
    //                moveDirection *= speed;
    //                anim.SetBool("IsRunning", true);
    //            }

    //        }
    //        moveDirection.y -= gravity * Time.deltaTime;
    //        controller.Move(moveDirection * Time.deltaTime);
    //    }
    //}



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
