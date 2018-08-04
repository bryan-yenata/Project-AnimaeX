using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderCharacter : MonoBehaviour {

    #region Variables
    //Character parameters
    public float percentage = 0.0f;
    public float weight = 5f;
	public float moveSpeed = 5f;
    public float jumpVelocity = 5f;
    public int doubleJump = 1;

    //Modifiers
    public bool canMove = true;

    public Quaternion lookLeft;
    public Quaternion lookRight;
    public Vector3 moveDirection = Vector3.zero;

    //Cached components
    public Rigidbody2D rigidBody;
    public Transform myTransform;
    //public Animator animator;
    public CharacterController characterController;
    #endregion

    #region Custom Methods

    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        myTransform = transform;

        Cursor.visible = false;
        //animator = GetComponent<Animator>();

        lookRight = transform.rotation;
        lookLeft = lookRight * Quaternion.Euler(0, 180, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	#endregion
}
