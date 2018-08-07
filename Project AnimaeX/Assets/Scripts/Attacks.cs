using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : AttackParameters {

	#region Variables
	
	#endregion

	#region Custom Methods
    public void Jab()
    {

    }
	#endregion

	#region Unity Methods

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                Debug.Log(other.gameObject.GetComponent<PlayerController>());
                Debug.Log(other.gameObject.GetComponent<CharacterParameters>());
            }

        }
    }

    #endregion
}
