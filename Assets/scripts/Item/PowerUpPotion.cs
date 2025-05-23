using System;
using UnityEngine;

public class PowerUpPotion : Item
{
    public int duration = 10;
    protected override void UseItem(PlayerController player)
    {
        player.PowerUp(duration);
        Debug.Log("PowerUpPotion ����! ���� �ð�: " + duration + "��");

        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxItem_Power);
    }

    
}
