using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {
    public static float MAX_HEALTH = 100f;

    public float health = MAX_HEALTH;
    public string fighterName;
    public Fighter opponent;

    protected Animator animator;
    private Rigidbody2D myBody;

	// Use this for initialization
	void Start () {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("health", healthPercent);

        if (opponent == null)
        {
            animator.SetFloat("opponent_health", opponent.healthPercent);
        }
        else
        {
            animator.SetFloat("opponent_health", 1);
        }
	}

    public float healthPercent
    {
        get
        {
            return health / MAX_HEALTH;
        }
    }

    public Rigidbody2D body
    {
        get
        {
            return this.myBody;
        }
    }
}
