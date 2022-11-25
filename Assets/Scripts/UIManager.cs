using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score_Text;
    [SerializeField] private Sprite[] _liveSprites;

    [SerializeField] private Image _liveImage;

    // Start is called before the first frame update
    void Start()
    {
        _score_Text.text = "Score: " + 0;
    }

    public void UpdateScore(int playerScore)
    {
        _score_Text.text = "Score: " + playerScore.ToString();   
    }

    public void UpdateLives(int currentPlayerLives)
    { 
        _liveImage.sprite = _liveSprites[currentPlayerLives];
    }
}
