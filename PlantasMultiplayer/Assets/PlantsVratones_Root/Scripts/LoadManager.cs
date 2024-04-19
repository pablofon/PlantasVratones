using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public void SceneLoader(int sceneToLoad)
    {
        GameManager.Instance.GameReset();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void MainMenuSceneLoader(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitGame()
    {
        Debug.Log("saliendo de Juego");
        Application.Quit();
    }
}
