using UnityEngine;

public class Item_Score_1000 : Item
{
   

    protected override void UseItem(PlayerController player)
    {
        GameManager.score += 1000;
    }
}
