using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool _isDead = false;
    private bool _isWinner = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && (_isDead || _isWinner))
        {
            RestartLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            BackToMainMenu();
        }
    }

    public void GameOver()
    {
        _isDead = true;
    }

    public void Winner()
    {
        _isWinner = true;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }


    private void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
