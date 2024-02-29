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
    private Text _winnerText;
    [SerializeField]
    private Text _resetText;
    [SerializeField]
    private Text _mainMenuText;
    private bool _display = false;
    [SerializeField]
    private Scrollbar _shieldStrength;
    [SerializeField]
    private Scrollbar _thrusterVolume;
    [SerializeField]
    private Scrollbar _bossStrength;
    private float _bossScore = 100;
    private GameManager _gameManager;
    
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _ammoCountText.text = "Ammo: " + 200;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        StartCoroutine(FlickerCoroutine(_gameOverText));
        _resetText.gameObject.SetActive(true);
    }

    public void WinnerShow()
    {
        StartCoroutine(FlickerCoroutine(_winnerText));
        _resetText.gameObject.SetActive(true);
        _mainMenuText.gameObject.SetActive(true);
        _gameManager.Winner();
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

    public void UpdateThruster(float currentThrusterSize)
    {
        _thrusterVolume.size = currentThrusterSize;

        if (_thrusterVolume.size == 0)
        {
            _thrusterVolume.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            _thrusterVolume.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void UpdateBossScore()
    {
        _bossScore -= 3;
        _bossStrength.size = _bossScore/100;

        Debug.Log("Boss Scrollbar Size: " + _bossStrength.size);
        if (_bossStrength.size <= 0.01)
        {
            _bossStrength.gameObject.SetActive(false);
            WinnerShow();
        }
    }

    public void BossScore()
    {
        _bossStrength.gameObject.SetActive(true);
        StartCoroutine(BossAnnouncement());
    }

    IEnumerator BossAnnouncement()
    {
        int loop = 40;
        while (loop > 0)
        {

            if (_display)
            {
                _bossStrength.gameObject.SetActive(true);
            }
            else
            {
                _bossStrength.gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(0.3f);
            _display = !_display;

            loop--;
        }
    }

    IEnumerator FlickerCoroutine( Text text)
    {
        while(true)
        {

            text.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.2f);

            text.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.2f);


            /* alternative solution */

            /*if (_display)
            {
                text.gameObject.SetActive(true);
            }
            else
            {
                text.gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(0.2f);

            _display = !_display;*/
        }
    }

}
