using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public int score=0;
    public Text hp;
    public Text score_T;
    void UpdateHPText()
    {
        hp.text = "HP: " + playerController.HPOut().ToString();
    }

    void UpdateScore()
    {
        score_T.text = "Score : " + score.ToString();
    }

    void Update()
    {
        UpdateHPText();
        UpdateScore();
    }
}
