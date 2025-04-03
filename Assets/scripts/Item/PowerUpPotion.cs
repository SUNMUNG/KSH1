using System;
using UnityEngine;

public class PowerUpPotion : Item
{
    public int duration = 10;
    protected override void UseItem(PlayerController player)
    {
        player.PowerUp(duration);
        Debug.Log("PowerUpPotion 사용됨! 지속 시간: " + duration + "초");
    }
}
