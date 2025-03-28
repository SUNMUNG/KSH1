using UnityEngine;

public class HealthPotion : Item
{
    public int healthAmount = 2;  // 회복되는 HP 양

    protected override void UseItem(PlayerController player)
    {
        // HP를 증가시킴
        player.hp += healthAmount;

        // 증가한 HP 출력
        Debug.Log("Player's hp increased by " + healthAmount + ". New HP: " + player.hp);
    }
}