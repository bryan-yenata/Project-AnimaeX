using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : PhysicsOverride {

    #region Variables
    public float jumpTakeOffSpeed = 7f;
    public float jumpCancelModifier = 0.5f;
    public Vector2 knockback;
    


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
                animator.SetBool("walk", false);
                character.running = true;
            }
            else if (Mathf.Abs(move.x) > 0 && Mathf.Abs(move.x) < 0.5 && grounded)
            {
                character.running = false;
                animator.SetBool("walk", true);
                animator.SetBool("run", false);
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
        
        if(knockback == Vector2.zero)
        {
            targetVelocity = move * character.moveSpeed;
        }
        else if(character.lookLeft)
        {
            targetVelocity.x = -knockback.x;
            targetVelocity.y = knockback.y;
        }
        else if (character.lookLeft == false)
        {
            targetVelocity.x = knockback.x;
            targetVelocity.y = knockback.y;
        }

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
            character.tempShield.transform.position += new Vector3(0, 1, 0);
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
        // Nair
        if (player.GetButtonDown("Normal Attack") && !grounded)
        {
            Debug.Log("IN Nair");
            StartCoroutine(Nair());
        }


        // Neutral B
        if (player.GetButtonDown("Special Attack") && grounded)
        {
            Debug.Log("IN Neutral B");
            StartCoroutine(NeutralB());
        }

        // Jab
        if (player.GetButtonDown("Normal Attack") && grounded)
        {
            Debug.Log("IN Jab");
            StartCoroutine(Jab());
            
        }

        
    }

    //Attacks Coroutines
    IEnumerator Nair()
    {
        Debug.Log("Nair");
        character.tempAirHitbox = Instantiate(character.airHitbox, new Vector3(0, 0, 0), transform.rotation) as GameObject;
        character.tempAirHitbox.transform.parent = gameObject.transform;

        character.tempAirHitbox.transform.position = gameObject.transform.position;

        character.canMove = false;
        animator.SetTrigger("neutral_air");
        
        animator.SetBool("run", false);
        character.running = false;
        animator.SetBool("walk", false);

        yield return new WaitForSeconds(0.5f);

        Destroy(character.tempAirHitbox);
        character.canMove = true;
    }
    IEnumerator NeutralB()
    {
        Debug.Log("Neutral B");
        character.tempBHitbox = Instantiate(character.bHitbox, new Vector3(0, 0, 0), transform.rotation) as GameObject;
        character.tempBHitbox.transform.parent = gameObject.transform;

        character.tempBHitbox.transform.position = gameObject.transform.position;

        character.canMove = false;
        animator.SetTrigger("neutral_special");
        animator.SetBool("run", false);
        character.running = false;
        animator.SetBool("walk", false);
        

        yield return new WaitForSeconds(0.5f);

        Destroy(character.tempBHitbox);
        character.canMove = true;
    }
    IEnumerator Jab()
    {
        Debug.Log("Jab");
        character.tempAHitbox = Instantiate(character.aHitbox, new Vector3(0, 0, 0), transform.rotation) as GameObject;
        character.tempAHitbox.transform.parent = gameObject.transform;

        character.tempAHitbox.transform.position = gameObject.transform.position;

        character.canMove = false;
        animator.SetTrigger("punch");
        animator.SetBool("run", false);
        character.running = false;
        animator.SetBool("walk", false);

        character.jabOnce = true;

        yield return new WaitForSeconds(0.5f);

        Destroy(character.tempAHitbox);
        character.jabOnce = false;
        character.canMove = true;
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

    void Knockback(Vector2 knockback)
    {
        targetVelocity = knockback;
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

    protected override void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);


    }
}
    

    #endregion
