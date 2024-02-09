using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //handel to Text
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
    
    
   

    // Start is called before the first frame update
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

    public void GameOverShow() {
        
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


            /*
            _gameOverText.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.2f);

            _gameOverText.gameObject.SetActive(false);
            */

        }


    }

}
