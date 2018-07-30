using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsOverride : MonoBehaviour {

    #region Variables
    Rigidbody m_Rigidbody;

    #endregion

    #region Custom Methods

    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        //This locks the RigidBody so that it does not move or rotate in the Z axis.
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
    }

    // Update is called once per frame
    void Update () {
		
	}

	#endregion
}
