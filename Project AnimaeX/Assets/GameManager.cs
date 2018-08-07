using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Variables
    public int p1Stock = 3;
    public int p2Stock = 3;

    public Image heart11, heart12, heart13, heart21, heart22, heart23;


    public GameObject p1;
    public GameObject p2;

    public GameObject tempP1;
    public GameObject tempP2;

    public GameObject UI1;
    public GameObject UI2;

    public Text p1Per;
    public Text p2Per;

    public Text gameOver;
    public GameObject button;

    public bool p1Dead;
    public bool p2Dead;
    #endregion

    #region Custom Methods

    #endregion

    #region Unity Methods

    // Use this for initialization
    void Awake()
    {
        tempP1 = Instantiate(p1, new Vector3(-5, 0, 0), Quaternion.identity);
        heart11.gameObject.SetActive(true);
        heart12.gameObject.SetActive(true);
        heart13.gameObject.SetActive(true);
        


        tempP2 = Instantiate(p2, new Vector3(5, 0, 0), Quaternion.identity);
        heart21.gameObject.SetActive(true);
        heart22.gameObject.SetActive(true);
        heart23.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (p1Stock == 0)
        {
            UI1.gameObject.SetActive(false);
            UI2.gameObject.SetActive(false);
            gameOver.text = "Player 2 WINS!";
            gameOver.gameObject.SetActive(true);
            button.gameObject.SetActive(true);

        }
        if (p2Stock == 0)
        {
            UI1.gameObject.SetActive(false);
            UI2.gameObject.SetActive(false);
            gameOver.text = "Player 1 WINS!";
            gameOver.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
        }

        switch (p1Stock)
        {
            case 3:
                heart11.gameObject.SetActive(true);
                heart12.gameObject.SetActive(true);
                heart13.gameObject.SetActive(true);
                break;
            case 2:
                heart11.gameObject.SetActive(true);
                heart12.gameObject.SetActive(true);
                heart13.gameObject.SetActive(false);
                break;
            case 1:
                heart11.gameObject.SetActive(true);
                heart12.gameObject.SetActive(false);
                heart13.gameObject.SetActive(false);
                break;
            case 0:
                heart11.gameObject.SetActive(false);
                heart12.gameObject.SetActive(false);
                heart13.gameObject.SetActive(false);
                break;
        }
        switch (p2Stock)
        {
            case 3:
                heart21.gameObject.SetActive(true);
                heart22.gameObject.SetActive(true);
                heart23.gameObject.SetActive(true);
                break;
            case 2:
                heart21.gameObject.SetActive(true);
                heart22.gameObject.SetActive(true);
                heart23.gameObject.SetActive(false);
                break;
            case 1:
                heart21.gameObject.SetActive(true);
                heart22.gameObject.SetActive(false);
                heart23.gameObject.SetActive(false);
                break;
            case 0:
                heart21.gameObject.SetActive(false);
                heart22.gameObject.SetActive(false);
                heart23.gameObject.SetActive(false);
                Time.timeScale = 0;
                break;
        }

        if(!p1Dead && !p2Dead)
        {

            p1Per.text = tempP1.GetComponent<PlayerController>().percentage.ToString() + " %";

            p2Per.text = tempP2.GetComponent<PlayerController>().percentage.ToString() + " %";
        }
        if (p1Dead && p1Stock > 0)
        {
            p1Dead = false;
            tempP1 = Instantiate(p1, new Vector3(-5, 0, 0), Quaternion.identity);
        }
        if (p2Dead && p2Stock > 0)
        {
            p2Dead = false;
            tempP2 = Instantiate(p2, new Vector3(5, 0, 0), Quaternion.identity);
        }

        
    }

	#endregion
}
