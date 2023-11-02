using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private string loadingScreenSceneName = "LoadingScreen";
    
    public void LoadLoadingScreenScene()
    {
        SceneManager.LoadScene(loadingScreenSceneName);
    }
    
    public static void QuitGame()
    {
        Application.Quit();
    }
    

}
