using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController2 : PhysicsOverride {

    #region Variables
    public float jumpTakeOffSpeed = 7f;
    public float jumpCancelModifier = 0.5f;
    public float shortHopMultiplier = 0.5f;
    public Vector2 knockback;
    public float knockbackDuration;

    //Movement stuff
    public float horizontalMove = 0f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;



    //Character parameters
    public float percentage = 0.0f;
    public int stocks = 3;
    public int doubleJumpLeft = 1;

    //Character components
    public CharacterParameters character;
    public Animator animator;
    public Rigidbody2D rigidbody;

    //Rewired
    public int playerId;
    private Rewired.Player player;

    //Boolean
    public bool attacked = false;
    public bool hit = false;
    public bool knockbackLeft = false;

    #endregion

    #region Custom Methods
    /*
    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        if(character.canMove == true)
        {
            move.x = player.GetAxis("Horizontal");

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
            if (player.GetButtonDown("Jump") && doubleJumpLeft >= 0)
            {
                animator.SetBool("jump", true);
                velocity.y = character.jumpVelocity;
                rigidbody.AddForce(Vector2.up);
                doubleJumpLeft -= 1;
                character.onAir = true;
            }
            else if (player.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * jumpCancelModifier;
                }
            }

            else if (character.onAir == false)
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
    */

    //Move and Movement v2 (for rigidbody)

    void Move()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * character.moveSpeed;

        if (character.canMove == true)
        {

            //Moving
            if (player.GetAxis("Horizontal") >= 0.5 && grounded)
            {
                animator.SetBool("run", true);
                animator.SetBool("walk", false);
                character.running = true;
            }
            else if (player.GetAxis("Horizontal") > 0 && player.GetAxis("Horizontal") < 0.5 && grounded)
            {
                character.running = false;
                animator.SetBool("walk", true);
                animator.SetBool("run", false);
            }
            if (player.GetAxis("Horizontal") <= 0.5 && grounded)
            {
                animator.SetBool("run", true);
                animator.SetBool("walk", false);
                character.running = true;
            }
            else if (player.GetAxis("Horizontal") < 0 && player.GetAxis("Horizontal") > 0.5 && grounded)
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


            

        }

        else if (character.shielding)
        {
            if (player.GetAxis("Horizontal") > 0 && character.rollDodging == false)
            {
                animator.SetTrigger("roll_dodge");
                rigidbody.AddForce(new Vector2(10, 0));

            }
            else if (player.GetAxis("Horizontal") < 0 && character.rollDodging == false)
            {
                animator.SetTrigger("roll_dodge");
                rigidbody.AddForce(new Vector2(-10, 0));
            }
        }
    }

    void Movement()
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(horizontalMove * Time.fixedDeltaTime * 10f, rigidbody.velocity.y);
        // And then smoothing it out and applying it to the character
        rigidbody.velocity = Vector2.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, movementSmoothing);

        //Jumping
        if (player.GetButtonDown("Jump") && doubleJumpLeft >= 0)
        {
            animator.SetBool("jump", true);
            rigidbody.AddForce(new Vector2(0, character.jumpVelocity));
            doubleJumpLeft -= 1;
            character.onAir = true;
        }
        else if (player.GetButtonUp("Jump"))
        {
            if (rigidbody.velocity.y > 0)
            {
                rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (shortHopMultiplier - 1) * Time.deltaTime;
            }
        }

        else if (character.onAir == false)
        {
            animator.SetBool("jump", false);
            doubleJumpLeft = character.doubleJump;
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
        if (player.GetButtonDown("Special Attack"))
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
        if (!attacked)
        {
            Debug.Log("Nair");
            attacked = true;
            character.tempAirHitbox = Instantiate(character.airHitbox, new Vector3(0, 0, 0), transform.rotation) as GameObject;
            character.tempAirHitbox.transform.parent = gameObject.transform;
            if (character.lookLeft == true)
            {
                character.tempAirHitbox.GetComponent<Attacks>().left = true;
            }
            else if (character.lookLeft == false)
            {
                character.tempAirHitbox.GetComponent<Attacks>().left = false;
            }

            character.tempAirHitbox.transform.position = gameObject.transform.position;
            character.tempAirHitbox.transform.rotation = gameObject.transform.rotation;

            animator.SetTrigger("aerial_attack");

            animator.SetBool("run", false);
            character.running = false;
            animator.SetBool("walk", false);

            yield return new WaitForSeconds(0.75f);

            
            Destroy(character.tempAirHitbox);
            attacked = false;
        }
        
    }
    IEnumerator NeutralB()
    {
        if (!attacked)
        {
            Debug.Log("Neutral B");
            attacked = true;
            character.tempBHitbox = Instantiate(character.bHitbox, new Vector3(0, 0, 0), transform.rotation) as GameObject;
            character.tempBHitbox.transform.parent = gameObject.transform;
            if (character.lookLeft == true)
            {
                character.tempBHitbox.GetComponent<Attacks>().left = true;
            }
            else if (character.lookLeft == false)
            {
                character.tempBHitbox.GetComponent<Attacks>().left = false;
            }

            character.tempBHitbox.transform.position = gameObject.transform.position;
            character.tempBHitbox.transform.rotation = gameObject.transform.rotation;

            character.canMove = false;
            animator.SetTrigger("neutral_special");
            animator.SetBool("run", false);
            character.running = false;
            animator.SetBool("walk", false);


            yield return new WaitForSeconds(0.55f);

            Destroy(character.tempBHitbox);
            attacked = false;
            character.canMove = true;
        }
    }
    IEnumerator Jab()
    {
        if (!attacked)
        {
            Debug.Log("Jab");
            attacked = true;
            character.tempAHitbox = Instantiate(character.aHitbox, new Vector3(0, 0, 0), transform.rotation) as GameObject;
            character.tempAHitbox.transform.parent = gameObject.transform;
            if(character.lookLeft == true)
            {
                character.tempAHitbox.GetComponent<Attacks>().left = true;
            }
            else if (character.lookLeft == false)
            {
                character.tempAHitbox.GetComponent<Attacks>().left = false;
            }

            character.tempAHitbox.transform.position = gameObject.transform.position;
            character.tempAHitbox.transform.rotation = gameObject.transform.rotation;

            character.canMove = false;
            animator.SetTrigger("punch");
            animator.SetBool("run", false);
            character.running = false;
            animator.SetBool("walk", false);

            character.jabOnce = true;

            yield return new WaitForSeconds(0.25f);

            Destroy(character.tempAHitbox);
            character.jabOnce = false;
            character.canMove = true;
            attacked = false;
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

        if (!attacked)
        {
            if (character.lookLeft)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }



        }
    }

    /*
    IEnumerator Knockback()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

        velocity = knockback;

        Debug.Log("Magnitude:" + knockback.magnitude);
        Debug.Log("Duration:" + knockbackDuration);

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = Vector2.right * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);

        yield return new WaitForSeconds(knockbackDuration);

        knockback = Vector2.zero;
        velocity = Vector2.zero;
        hit = false;
    }
    */

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

        Move();
        Shield();
        Attack();
        Flip();
    }

    protected override void FixedUpdate()
    {
        Movement();
        /*
            velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
            velocity.x = targetVelocity.x;

            grounded = false;

            Vector2 deltaPosition = velocity * Time.deltaTime;

            Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

            Vector2 move = moveAlongGround * deltaPosition.x;

            Movement(move, false);

            move = Vector2.up * deltaPosition.y;

            Movement(move, true);

            if (grounded)
            {
                character.onAir = false;
            }

        */
        /*
        else if (hit)
        {
            rigidbody.AddForce(knockback);            
        }
        */
        
    }
}
    

    #endregion
