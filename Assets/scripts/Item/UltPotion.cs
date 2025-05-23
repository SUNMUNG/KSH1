using UnityEngine;

public class UltPotion : Item
{
    public int ultamount=25;

    protected override void UseItem(PlayerController player)
    {
        player.Ult += ultamount;
        Debug.Log("�÷��̾��� �ñر��ġ"+ultamount+" ����");

        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxItem_Ult);

    }
}
