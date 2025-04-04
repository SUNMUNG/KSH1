using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Hp=5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 2f;
    public float fireRate = 0.5f;
    public float speed = 5f;
    public float nextFireTime = 0f;
    public float moveDirection = 1f;

    public GameObject reward_1;
    private Animator animator;
    private Collider2D enemyCollider;
    public void Start()
    {
     animator = GetComponent<Animator>();
     enemyCollider = GetComponent<Collider2D>();

    }

    public void damaged(float damage)
    {
        Hp -= damage; 

        if (Hp <= 0)
        {
            //폭발 애니메이션동안 남아있는 콜라이더가 총알을 막을수있으므로
            if (enemyCollider != null)
                enemyCollider.enabled = false;
            reward();
            animator.SetBool("isdie",true); //애니메이션 트리거
            Destroy(gameObject,1f); //enemy 게임오브젝트 제거;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bulletborder")
        {
            Debug.Log(collision.gameObject.tag);
            Destroy(gameObject);
        }
    }
    public virtual void shoot() { }

    public virtual void move() { }

    public virtual void reward() {
        // 아이템을 생성하는 위치
        GameObject reward1 = Instantiate(reward_1, transform.position, Quaternion.identity);

        // 아이템이 생성되면, PlayerController가 제대로 연결되었는지 확인
        if (reward1 != null)
        {
            Debug.Log("Reward item created successfully.");

            // 만약 PlayerController가 아이템과 충돌한 상태에서 사용할 경우, 그와의 연결을 다시 체크
            Item_Score_100 itemScript = reward1.GetComponent<Item_Score_100>();
            if (itemScript != null)
            {
                Debug.Log("Item_Score_100 script found.");
            }
            else
            {
                Debug.LogError("Item_Score_100 script not found on reward item.");
            }
        }
    }


}
