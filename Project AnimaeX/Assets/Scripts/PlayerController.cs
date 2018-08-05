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

            if (player.GetButtonDown("Jump") && doubleJumpLeft > 0)
            {
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
                doubleJumpLeft = character.doubleJump;
            }

            targetVelocity = move * character.moveSpeed;
        }

        else if (character.shielding)
        {

        }
        
    }

    void KnockBack(Vector2 knockback)
    {
        velocity = knockback;
    }

    void Shield()
    {
        if (player.GetButtonDown("Shield") && grounded)
        {
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
    }
}
    

    #endregion
