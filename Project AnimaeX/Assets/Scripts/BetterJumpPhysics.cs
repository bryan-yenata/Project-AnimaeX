using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumpPhysics : MonoBehaviour {

    #region Variables
    public float fallMultiplier = 2.5f;
    public float shortHopMultiplier = 1.5f;

    Rigidbody rigidBody;
	#endregion

	#region Custom Methods

	#endregion

	#region Unity Methods

	// Use this for initialization
	void Awake () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(rigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * (shortHopMultiplier - 1) * Time.deltaTime;
        }
	}   

	#endregion
}
