using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saitama : CharacterParameters {

    #region Variables

    #endregion

    #region Custom Methods
    protected Vector2[] Jab()
    {
        Vector2 knockbackDirection = new Vector2(1, 0);
        Vector2 damage = new Vector2(4, 0);

        Vector2[] result = new Vector2[2] { knockbackDirection, damage };
        return result;
    }
    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Reset()
    {
        characterName = "Saitama";
    }
    #endregion
}
