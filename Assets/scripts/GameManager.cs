using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public Text hp;
    void UpdateHPText()
    {
        // PlayerController에서 현재 HP 값을 가져와서 텍스트에 반영
        hp.text = "HP: " + playerController.HPText().ToString();
    }

    void Update()
    {
        UpdateHPText();
    }
}
