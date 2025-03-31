using UnityEngine;

public class Item_Score_100 : Item
{
   

    protected override void UseItem(PlayerController player)
    {
        GameManager.score += 100;
    }
}
