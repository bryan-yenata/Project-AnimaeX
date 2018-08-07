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
    public GameObject aHitbox;
    public GameObject tempAHitbox;
    public GameObject bHitbox;
    public GameObject tempBHitbox;
    public GameObject airHitbox;
    public GameObject tempAirHitbox;


    //Cached components
    public Transform myTransform;
    #endregion

    #region Custom Methods

    //General Movements
    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start () {
    }
    

    #endregion
}
