using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : Attacks {

    #region Variables
    public float speed = 1;
	#endregion

	#region Custom Methods
    void SelfDestroy()
    {
        Destroy(gameObject, .3f);
    }
	#endregion

	#region Unity Methods

	// Use this for initialization
	void Start () {
        knockbackDirection = new Vector2(xDir, yDir);
        if (left)
        {
            speed = -speed;
        }
        
        Invoke("SelfDestroy", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed,0);
    }

    #endregion
}
