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
            //���� �ִϸ��̼ǵ��� �����ִ� �ݶ��̴��� �Ѿ��� �����������Ƿ�
            if (enemyCollider != null)
                enemyCollider.enabled = false;
            reward();
            animator.SetBool("isdie",true); //�ִϸ��̼� Ʈ����
            AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxExplosion);
            Destroy(gameObject,1f); //enemy ���ӿ�����Ʈ ����;
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
        // �⺻ ������ �׻� ����
        GameObject reward1 = Instantiate(reward_1, transform.position, Quaternion.identity);
        if (reward1 != null)
        {
            Debug.Log("Reward 1 created successfully.");
        }

        // Ȯ���� reward_2 ���� (��: 30% Ȯ��)
        float chance2 = Random.Range(0f, 1f);
        if (chance2 <= 0.3f)
        {
            GameObject reward2 = Instantiate(reward_2, transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
            Debug.Log("Reward 2 created!");
        }

        // Ȯ���� reward_3 ���� (��: 15% Ȯ��)
        float chance3 = Random.Range(0f, 1f);
        if (chance3 <= 0.15f)
        {
            GameObject reward3 = Instantiate(reward_3, transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
            Debug.Log("Reward 3 created!");
        }
    }
    public enum MovementPattern
    {
        Horizontal,  // ���ʿ��� ���������� �̵�
        Vertical,    // ������ �Ʒ��� �̵�
        Diagonal,    // �밢�� �������� �̵�
        Circular     // ���� ����
    }

    public MovementPattern movementPattern;  // �̵� ����
    public float radius = 5f;  // ���� ������ ���� ������

    private float timer = 0f;  // �ð� ������� ������ �����ϴ� Ÿ�̸�

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

    // ���� �̵�
    void MoveHorizontally()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    // ���� �̵�
    void MoveVertically()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    // �밢�� �̵�
    void MoveDiagonally()
    {
        transform.position += new Vector3(1, -1, 0) * speed * Time.deltaTime;
    }
    
    // ���� �������� �̵�
    void MoveInCircle()
    {
        timer += Time.deltaTime;
        float x = Mathf.Cos(timer * speed) * radius;
        float y = Mathf.Sin(timer * speed) * radius;
        transform.position = new Vector3(x, y, 0);
    }


}
