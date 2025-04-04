using UnityEngine;

public class Item_Score_100 : Item
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
        gameManager.score += 100;
        Debug.Log("Score added: " + gameManager.score);
    }
}