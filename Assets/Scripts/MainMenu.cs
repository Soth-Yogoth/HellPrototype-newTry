using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static void OnStartButtonPressed()
    {
        //Scene scene = SceneManager.GetSceneByBuildIndex(1);
        SceneManager.LoadScene("GameLoop", LoadSceneMode.Single);
    }
    
    public static void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
