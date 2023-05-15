using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] string mainGame = "";
    
    public void StartGame()
    {
        SceneManager.LoadScene(mainGame, LoadSceneMode.Single);
    }
    public void QuitApplication()
    {
        QuitApplication();
    }


}
