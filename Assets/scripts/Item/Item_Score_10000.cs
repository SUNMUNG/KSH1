using UnityEngine;

public class Item_Score_10000 : Item
{
    protected override void UseItem(PlayerController player)
    {


        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager instance is not found.");
            return;  // GameManager�� ���ٸ� �� �̻� �������� ����
        }

        // ���� �߰�
        gameManager.score += 10000;
        Debug.Log("Score added: " + gameManager.score);
    }
}
