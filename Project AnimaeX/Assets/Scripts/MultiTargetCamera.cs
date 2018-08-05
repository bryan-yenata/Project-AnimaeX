using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiTargetCamera : MonoBehaviour {

    #region Variables
    public List<Transform> targets;

    public Vector3 offset;
    public float smoothTime = 0.3f;

    public float minZoom = 55f;
    public float maxZoom = 20f;
    public float zoomLimiter = 15f;

    private Vector3 velocity;
    private Camera cam;

    //Camera Movement Restrictions
    public float topBoundaryCam = 8f;
    public float bottomBoundaryCam = 0f;
    public float leftBoundaryCam = -7f;
    public float rightBoundaryCam = 7f;

    #endregion

    #region Custom Methods
    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, leftBoundaryCam, rightBoundaryCam);
        clampedPosition.y = Mathf.Clamp(transform.position.y, bottomBoundaryCam, topBoundaryCam);
        transform.position = clampedPosition;
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }
    
    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        if(targets.Count == 0)
        {
            return;
        }

        Move();
        Zoom();
    }
    #endregion
}
