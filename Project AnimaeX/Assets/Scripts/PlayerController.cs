using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : PhysicsOverride {

    #region Variables
    public float jumpTakeOffSpeed = 7f;
    public float jumpCancelModifier = 0.5f;

    //Character parameters
    public float percentage = 0.0f;
    public int stocks = 3;
    private int doubleJumpLeft = 1;

    //Character components
    public CharacterParameters character;
    public Animator animator;

    //Rewired
    public int playerId;
    private Rewired.Player player;

    #endregion

    #region Custom Methods
    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        if(character.canMove == true)
        {
            move.x = player.GetAxis("Horizontal");

            //Crouching
            if (player.GetAxis("Vertical") < 0)
            {
                animator.SetBool("crouch", true);
            }
            else
            {
                animator.SetBool("crouch", false);
            }

            //Moving
            if (Mathf.Abs(move.x) >= 0.5 && grounded)
            {
                animator.SetBool("run", true);
                character.running = true;
            }
            else if (Mathf.Abs(move.x) > 0 && Mathf.Abs(move.x) < 0.5 && grounded)
            {
                animator.SetBool("walk", true);
            }
            else
            {
                animator.SetBool("run", false);
                character.running = false;
                animator.SetBool("walk", false);
            }

            
            //Jumping
            if (player.GetButtonDown("Jump") && doubleJumpLeft > 0)
            {
                animator.SetBool("jump", true);
                velocity.y = character.jumpVelocity;
                doubleJumpLeft -= 1;
            }
            else if (player.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * jumpCancelModifier;
                }
            }

            if (grounded)
            {
                animator.SetBool("jump", false);
                doubleJumpLeft = character.doubleJump;
            }




            
        }

        else if (character.shielding)
        {
            if (player.GetAxis("Horizontal")>0 && character.rollDodging == false)
            {
                animator.SetTrigger("roll_dodge");
                move = new Vector2(3,0);
            }
            else if(player.GetAxis("Horizontal")<0 && character.rollDodging == false)
            {
                animator.SetTrigger("roll_dodge");
                move = new Vector2(-3, 0);
            }
        }

        targetVelocity = move * character.moveSpeed;

    }

    void KnockBack(Vector2 knockback)
    {
        velocity = knockback;
    }

    void Shield()
    {
        if (player.GetButtonDown("Shield") && grounded)
        {
            Debug.Log("Shielding");
            character.canMove = false;
            character.shielding = true;
            character.tempShield = Instantiate(character.shield, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            character.tempShield.transform.parent = gameObject.transform;

            character.tempShield.transform.position = gameObject.transform.position;
        }
        else if (player.GetButtonUp("Shield"))
        {
            Destroy(character.tempShield);
            character.canMove = true;
            character.shielding = false;
        }



    }
    
    void Attack()
    {
        // Jab
        if (player.GetButtonDown("Normal Attack") && grounded)
        {
            
        }
    }

    void Flip()
    {
        if(player.GetAxis("Horizontal") < 0)
        {
            character.lookLeft = true;
        }
        else if(player.GetAxis("Horizontal") > 0)
        {
            character.lookLeft = false;
        }

        if (character.lookLeft)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    #endregion

    #region Unity Methods

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    // Use this for initialization
    void Start ()
    {
		
	}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ledge"))
        {
            if (velocity.y >= 0)
                velocity.y += 10;

            else
                velocity.y = -velocity.y;
        }
    }

    protected override void Update()
    {
        base.Update();

        Shield();
        Attack();
        Flip();
    }
}
    

    #endregion
