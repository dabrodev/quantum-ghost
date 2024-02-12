using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _resetText;
    private bool _display = false;
    
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
    }

    public void GameOverShow()
    {
        StartCoroutine(GameOverFlickerCoroutine());
        _resetText.gameObject.SetActive(true);
    }

    IEnumerator GameOverFlickerCoroutine()
    {
        while(true)
        {
            if (_display)
            {
                _gameOverText.gameObject.SetActive(true);
            }
            else
            {
                _gameOverText.gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(0.2f);

            _display = !_display;

            /* Below the alternative solution shown in video, 
               but it doesn't work for me

            _gameOverText.gameObject.SetActive(true);  

            yield return new WaitForSeconds(0.2f);

            _gameOverText.gameObject.SetActive(false);
            */

        }


    }

}
