using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParameters : MonoBehaviour {

    #region Variables
    //Character details
    public string characterName;
    
    //Character parameters
    public float percentage = 0.0f;
    public float weight = 5f;
	public float moveSpeed = 5f;
    public float jumpVelocity = 10f;
    public int doubleJump = 1;
    public float skillBar = 0; // range from 0 - 1000 (level 0 is 0-99, 1 is 100-199, ..., MAX is 1000)

    //Modifiers
    public bool canMove = true;
    public bool shielding = false;
    public bool invulnerable = false;
    public bool onAir = false;
    public bool running = false;
    public bool lookLeft = false;
    public bool rollDodging = false;
    public bool jabOnce = false;
    public bool attacked = false;

    //Prefabs
    public GameObject shield;
    public GameObject tempShield;
    public GameObject hitbox;
    public GameObject tempHitbox;

    //Cached components
    public Transform myTransform;
    #endregion

    #region Custom Methods
    //Knockback related
    public float KnockbackMagnitude(float p, float d, float w, float s, float b, float r)
    {
        /* 
         * r = series of ratios
         * b = base attack knockback
         * s = attack's knockback scaling (also known as knockback growth) divided by 100 (so a scaling of 110 is input as 1.1)
         * w = weight of target
         * d = damage of attack
         * p = percentage of target
        */
         
        float result = r * (b + (s * (18f + (1.4f * (200f / w + 100f) * (p / 10f + p * d / 20f)))));
        return result;
    }

    //General Movements
    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start () {
    }
    

    #endregion
}
