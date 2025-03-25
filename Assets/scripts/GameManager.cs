using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public Text hp;
    void UpdateHPText()
    {
        // PlayerController���� ���� HP ���� �����ͼ� �ؽ�Ʈ�� �ݿ�
        hp.text = "HP: " + playerController.HPText().ToString();
    }

    void Update()
    {
        UpdateHPText();
    }
}
