using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] string mainGame = "";

    private bool creditsVisible = false;
    [SerializeField] public GameObject credits;
    
    public void StartGame()
    {
        SceneManager.LoadScene(mainGame, LoadSceneMode.Single);
    }

    public void ToggleCredits()
    {
        creditsVisible = !creditsVisible;

        if (creditsVisible)
        {
            credits.SetActive(true);
        }
        else if(!creditsVisible)
        {
            credits.SetActive(false);
        }

    }


    public void QuitApplication()
    {
        QuitApplication();
    }


}
