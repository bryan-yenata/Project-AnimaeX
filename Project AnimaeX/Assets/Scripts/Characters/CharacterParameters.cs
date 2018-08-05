using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParameters : MonoBehaviour {

    #region Variables
    //Character parameters
    public float percentage = 0.0f;
    public float weight = 5f;
	public float moveSpeed = 5f;
    public float jumpVelocity = 5f;
    public int doubleJump = 1;

    //Modifiers
    public bool canMove = true;

    //Cached components
    public Transform myTransform;
    public CharacterController characterController;
    #endregion

    #region Custom Methods

    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start () {
    }
    

    #endregion
}
