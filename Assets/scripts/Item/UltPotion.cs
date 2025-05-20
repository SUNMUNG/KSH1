using UnityEngine;

public class UltPotion : Item
{
    public int ultamount=25;

    protected override void UseItem(PlayerController player)
    {
        player.Ult += ultamount;
        Debug.Log("플레이어의 궁극기수치"+ultamount+" 증가");
    }
}
