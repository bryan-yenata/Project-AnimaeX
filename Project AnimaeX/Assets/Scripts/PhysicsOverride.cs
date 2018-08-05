using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsOverride : MonoBehaviour {

    #region Variables
    public float gravityModifier = 1f;
    public float minGroundNormalY = 0.65f;

    protected float fixedGravity;

    //Protected variables
    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rigidBody;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    //Protected constants
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    #endregion

    #region Custom Methods
    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if(distance > minMoveDistance)
        {
            int count = rigidBody.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;

                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;

                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if(projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rigidBody.position = rigidBody.position + move.normalized * distance;

    }


    protected virtual void ComputeVelocity()
    {

    }

   
    #endregion

    #region Unity Methods

    void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody2D>(); 
    }

    // Use this for initialization
    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;

        fixedGravity = gravityModifier * Physics2D.gravity.y;
    }

    // Update is called once per frame
    protected virtual void Update () {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
	}

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move,true);

    }

    

    #endregion
}
