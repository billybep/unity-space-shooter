using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score_Text;

    // Start is called before the first frame update
    void Start()
    {
        _score_Text.text = "Score: " + 0;
    }

    public void UpdateScore(int playerScore)
    {
        _score_Text.text = "Score: " + playerScore.ToString();   
    }
}
