using UnityEngine;

public class HealthPotion : Item
{
    public int healthAmount = 2;  // ȸ���Ǵ� HP ��

    protected override void UseItem(PlayerController player)
    {
        // HP�� ������Ŵ
        player.hp += healthAmount;

        // ������ HP ���
        Debug.Log(healthAmount + "���� Hp: " + player.hp);
    }
}