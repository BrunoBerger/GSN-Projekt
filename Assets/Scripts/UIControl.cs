using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("t1");
        Application.Quit();
    }

    public void RestartLevel()
    {
        Debug.Log("t2");
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

}
