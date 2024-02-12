using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool _isDead = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isDead)
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
    }

    public void GameOver()
    {
        _isDead = true;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
