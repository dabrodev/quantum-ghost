using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _ammoCountText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _resetText;
    private bool _display = false;
    [SerializeField]
    private Scrollbar _shieldStrength;
    
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _ammoCountText.text = "Ammo: " + 15;
        _gameOverText.gameObject.SetActive(false);
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateAmmo(int ammoCount)
    {
        _ammoCountText.text = "Ammo: " + ammoCount.ToString();

        if (ammoCount == 0)
        {
            _ammoCountText.color = Color.red;
        }
        else
        {
            _ammoCountText.color = Color.white;
        }

    }

    public void UpdateLives(int currentLives)
    {
        Debug.Log("Number of Lives: " + currentLives);
        _livesImg.sprite = _liveSprites[currentLives];
    }

    public void GameOverShow()
    {
        StartCoroutine(GameOverFlickerCoroutine());
        _resetText.gameObject.SetActive(true);
    }

    public void ShowShieldStrength()
    {
        _shieldStrength.gameObject.SetActive(true);
        _shieldStrength.size = 1f;
    }

    public void DecreaseShieldStrength()
    {
        Debug.Log("Decrease Shield called");

        _shieldStrength.size -= 0.33f;

        if (_shieldStrength.size < 0.33f)
        {
            _shieldStrength.gameObject.SetActive(false);
        }
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
