// Item.cs
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameManager GameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� PlayerController ������Ʈ�� �����ɴϴ�
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                // �ڽ� Ŭ�������� UseItem ȣ��
                UseItem(player);
            }
            else
            {
                Debug.LogWarning("PlayerController not found on the player!");
            }

            // ������ ����
            Destroy(gameObject);
        }
    }

    // ������ ��� ó�� (�ڽ� Ŭ�������� �������̵��ؾ� �մϴ�)
    protected virtual void UseItem(PlayerController player)
    {
    }
}