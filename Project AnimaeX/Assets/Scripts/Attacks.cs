using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : AttackParameters
{

    #region Variables
    public bool left = false;
    public float knockbackDuration;
    public float knockbackMagnitude;
    Vector2 knockbackDirection;

    Vector2 playerKnockback;
    bool playerHit;
    #endregion

    #region Custom Methods
    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start()
    {
        knockbackDirection = new Vector2(xDir, yDir);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponentInChildren<PlayerController>();
            player.hit = true;
            playerHit = true;

            knockbackMagnitude = KnockbackMagnitude(
                    player.percentage, //percentage of target
                    d, //damage of attack
                    player.character.weight, // weight of target
                    0.1f,  //attack's knockback scaling [FIXED for now]
                    b,  //base attack knockback
                    0.1f); //series of ratio [FIXED to 1 for now]

            knockbackDuration = KnockbackDuration(player.percentage, knockbackMagnitude);

            if (left)
            {

                
                knockbackDirection.x = -knockbackDirection.x;
                player.knockback = knockbackMagnitude * knockbackDirection;
                player.knockbackDuration = knockbackDuration;
                player.percentage += d;
            }

            else if (!left)
            {

                
                player.knockback = knockbackMagnitude * knockbackDirection;
                player.knockbackDuration = knockbackDuration;
                player.percentage += d;


            }

        }
    }

    #endregion
}
