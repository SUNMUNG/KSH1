using UnityEngine;

public class Item_Score_1000 : Item
{
    protected override void UseItem(PlayerController player)
    {


        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager instance is not found.");
            return;  // GameManager�� ���ٸ� �� �̻� �������� ����
        }
        
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxItem_Score_Medium);

        // ���� �߰�
        gameManager.score += 1000;
        Debug.Log("Score added: " + gameManager.score);
    }
}
