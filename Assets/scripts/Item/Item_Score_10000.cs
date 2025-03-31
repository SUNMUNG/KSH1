using UnityEngine;

public class Item_Score_10000 : Item
{
   

    protected override void UseItem(PlayerController player)
    {
        GameManager.score += 10000;
    }
}
