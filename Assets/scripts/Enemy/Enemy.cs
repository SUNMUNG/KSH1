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
    public GameObject reward_2;
    public GameObject reward_3;
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

    public virtual void reward()
    {
        // 기본 보상은 항상 생성
        GameObject reward1 = Instantiate(reward_1, transform.position, Quaternion.identity);
        if (reward1 != null)
        {
            Debug.Log("Reward 1 created successfully.");
        }

        // 확률로 reward_2 생성 (예: 30% 확률)
        float chance2 = Random.Range(0f, 1f);
        if (chance2 <= 0.3f)
        {
            GameObject reward2 = Instantiate(reward_2, transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
            Debug.Log("Reward 2 created!");
        }

        // 확률로 reward_3 생성 (예: 15% 확률)
        float chance3 = Random.Range(0f, 1f);
        if (chance3 <= 0.15f)
        {
            GameObject reward3 = Instantiate(reward_3, transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
            Debug.Log("Reward 3 created!");
        }
    }


}
