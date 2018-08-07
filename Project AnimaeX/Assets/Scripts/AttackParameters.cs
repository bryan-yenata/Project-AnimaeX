using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackParameters : MonoBehaviour {

    #region Variables
    public string attackName;
    public float d = 999;
    public float b = 10;


    public float xDir = 0;
    public float yDir = 1;
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

        float result = r * (b + (s * (18f + (1.4f * (200f / w + 100f) * (p + p * (d / 2718))))));
        return result;
    }

    public float KnockbackDuration(float p, float m)
    {
        /* 
         * r = series of ratios
         * b = base attack knockback
         * s = attack's knockback scaling (also known as knockback growth) divided by 100 (so a scaling of 110 is input as 1.1)
         * w = weight of target
         * d = damage of attack
         * p = percentage of target
        */

        float result = 1.1f*(0.1f + p) / m;
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

	#endregion
}
