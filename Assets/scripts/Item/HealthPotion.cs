using UnityEngine;

public class HealthPotion : Item
{
    public int healthAmount = 1;  // 회복되는 HP 양

    protected override void UseItem(PlayerController player)
    {
        // HP를 증가시킴
        if (player.hp >= player.maxHP)
        {
            Debug.Log("이미 최대체력 " + player.maxHP + "입니다.");
            return;
        }
        else {
            player.hp += healthAmount;
            // 증가한 HP 출력
            Debug.Log(healthAmount + "현재 Hp: " + player.hp);
        }
        


        
    }
}