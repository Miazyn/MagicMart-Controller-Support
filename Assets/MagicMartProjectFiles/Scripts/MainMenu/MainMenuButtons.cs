using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] string mainGame = "";

    [SerializeField] public GameObject credits;
    
    public void StartGame()
    {
        SceneManager.LoadScene(mainGame, LoadSceneMode.Single);
    }

    public void ToggleCredits()
    {
        credits.SetActive(credits.activeSelf ? false : true);
    }

    public void QuitApplication()
    {
        QuitApplication();
    }


}
