using System;
using UnityEngine;

public class PowerUpPotion : Item
{
    public int duration = 60;
    protected override void UseItem(PlayerController player)
    {
        player.PowerUp(duration);
    }
}
