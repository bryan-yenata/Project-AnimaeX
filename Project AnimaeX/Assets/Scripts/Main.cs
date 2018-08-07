using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

<<<<<<< HEAD
    public void PlayGame ()
=======
    public void PlayGame1 ()
>>>>>>> UI
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

<<<<<<< HEAD
=======
    public void PlayGame2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void PlayGame3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void PlayGame4()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

>>>>>>> UI
    public void QuitGame ()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
