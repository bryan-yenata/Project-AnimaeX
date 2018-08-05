using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastZone : MonoBehaviour {

    #region Variables
    public MultiTargetCamera mtCamera;

    //Blast Zone position
    public float topBoundary = 18f;
    public float bottomBoundary = -18f;
    public float leftBoundary = -27f;
    public float rightBoundary = 27f;

    //Game Object transform
    public Transform charTransform;
    #endregion

    #region Custom Methods
    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start () {
        Debug.Log(mtCamera);

        mtCamera.targets.Add(gameObject.transform);
	}
	
	// Update is called once per frame
	void Update () {
		if(charTransform.position.x < leftBoundary || charTransform.position.x > rightBoundary || charTransform.position.y < bottomBoundary || charTransform.position.y > topBoundary)
        {
            Destroy(gameObject);
            mtCamera.targets.Remove(gameObject.transform);
        }
	}

	#endregion
}
