using UnityEngine;

public class HealthPotion : Item
{
    public int healthAmount = 1;  // ȸ���Ǵ� HP ��

    protected override void UseItem(PlayerController player)
    {
        // HP�� ������Ŵ
        if (player.hp >= player.maxHP)
        {
            Debug.Log("�̹� �ִ�ü�� " + player.maxHP + "�Դϴ�.");
            return;
        }
        else {
            player.hp += healthAmount;
            // ������ HP ���
            Debug.Log(healthAmount + "���� Hp: " + player.hp);
        }
        


        
    }
}