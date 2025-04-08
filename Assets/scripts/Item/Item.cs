// Item.cs
using UnityEngine;

public class Item : MonoBehaviour
{
    private float speed = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어에서 PlayerController 컴포넌트를 가져옵니다
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                // 자식 클래스에서 UseItem 호출
                Debug.Log("PlayerController detected. Using item.");
                UseItem(player);
            }
            else
            {
                Debug.LogWarning("PlayerController not found on the player!");
            }

            // 아이템 삭제
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag== "bulletborder")
        {
            Destroy(gameObject);
        }
    }

    public void movedown()
    {
        // Y축만 이동시키고 X축은 고정
        transform.position += (Vector3)Vector2.down * speed * Time.deltaTime;
    }

    private void Update()
    {
        movedown();
    }

    // 아이템 사용 처리 (자식 클래스에서 오버라이드해야 합니다)
    protected virtual void UseItem(PlayerController player)
    {
    }
}