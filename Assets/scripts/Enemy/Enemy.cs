using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Hp=5f;
    public GameObject bulletPrefab;
    public float bulletsize = 5f;
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
    public static bool isBulletBlocked = false;
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
    public enum MovementPattern
    {
        Horizontal,  // 왼쪽에서 오른쪽으로 이동
        Vertical,    // 위에서 아래로 이동
        Diagonal,    // 대각선 방향으로 이동
        Circular     // 원형 패턴
    }

    public MovementPattern movementPattern;  // 이동 패턴
    public float radius = 5f;  // 원형 패턴을 위한 반지름

    private float timer = 0f;  // 시간 기반으로 패턴을 제어하는 타이머

    void Update()
    {
        switch (movementPattern)
        {
            case MovementPattern.Horizontal:
                MoveHorizontally();
                break;
            case MovementPattern.Vertical:
                MoveVertically();
                break;
            case MovementPattern.Diagonal:
                MoveDiagonally();
                break;
            case MovementPattern.Circular:
                MoveInCircle();
                break;
        }
    }

    // 수평 이동
    void MoveHorizontally()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    // 수직 이동
    void MoveVertically()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    // 대각선 이동
    void MoveDiagonally()
    {
        transform.position += new Vector3(1, -1, 0) * speed * Time.deltaTime;
    }
    
    // 원형 패턴으로 이동
    void MoveInCircle()
    {
        timer += Time.deltaTime;
        float x = Mathf.Cos(timer * speed) * radius;
        float y = Mathf.Sin(timer * speed) * radius;
        transform.position = new Vector3(x, y, 0);
    }


}
