using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastZone : MonoBehaviour {

    #region Variables
    public MultiTargetCamera mtCamera;
    public GameManager manager;
    public PlayerController player;
    public CharacterParameters character;

    //Blast Zone position
    public float topBoundary = 18f;
    public float bottomBoundary = -18f;
    public float leftBoundary = -27f;
    public float rightBoundary = 27f;

    //Game Object transform
    public Transform charTransform;


    public GameObject explode;
    public GameObject tempExplode;
    #endregion

    #region Custom Methods
    #endregion

    #region Unity Methods

    // Use this for initialization
    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        mtCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MultiTargetCamera>();
    }

    void Start () {
        Debug.Log(mtCamera);

        mtCamera.targets.Add(gameObject.transform);
	}
	
	// Update is called once per frame
	void Update () {
		if(charTransform.position.x < leftBoundary || charTransform.position.x > rightBoundary || charTransform.position.y < bottomBoundary || charTransform.position.y > topBoundary)
        {

            tempExplode = Instantiate(explode, new Vector3(0, 0, 0), transform.rotation) as GameObject;
            tempExplode.transform.position = gameObject.transform.position;

            Destroy(gameObject);
            mtCamera.targets.Remove(gameObject.transform);

            
            if(player.playerId == 0)
            {
                manager.p1Stock -= 1;
                manager.p1Dead = true;
            }

            else if (player.playerId == 1)
            {
                manager.p2Stock -= 1;
                manager.p2Dead = true;
            }
            Destroy(tempExplode, 2f);
            
        }
	}

	#endregion
}
