using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float slideLifeTime = 3f;
    [SerializeField] private GameObject[] slides;
    
    private float timer = 0;
    private int slideCounter = 0;

    private Action showSlides;
    
    private void Update()
    {
        showSlides?.Invoke();
    }

    private void ShowSlides()
    {
        if (timer > 0) 
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = slideLifeTime;

            if (slideCounter < slides.Length)
            {
                slides[slideCounter].SetActive(true);
                slideCounter++;
            }
            else SceneManager.LoadScene("GameLoop", LoadSceneMode.Additive);
        }
    }

    public void OnStartButtonPressed()
    {
        showSlides = ShowSlides;
    }
    
    public static void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
