using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score_Text;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _restartGameText;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _liveImage;
    [SerializeField] private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _score_Text.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartGameText.gameObject.SetActive(false);

        _gameManager = GameObject
            .Find("Game_Manager")
            .GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is Null");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _score_Text.text = "Score: " + playerScore.ToString();   
    }

    public void UpdateLives(int currentPlayerLives)
    { 
        _liveImage.sprite = _liveSprites[currentPlayerLives];

        if (currentPlayerLives < 1)
        {
            StartCoroutine(GameOverFlickerRoutine());
        }
    }

    public void UpdateGameStatus()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartGameText.gameObject.SetActive(true);
        _gameManager.GameOver();
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
