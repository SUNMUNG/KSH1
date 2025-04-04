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
            //���� �ִϸ��̼ǵ��� �����ִ� �ݶ��̴��� �Ѿ��� �����������Ƿ�
            if (enemyCollider != null)
                enemyCollider.enabled = false;
            reward();
            animator.SetBool("isdie",true); //�ִϸ��̼� Ʈ����
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

    public virtual void reward() {
        // �������� �����ϴ� ��ġ
        GameObject reward1 = Instantiate(reward_1, transform.position, Quaternion.identity);

        // �������� �����Ǹ�, PlayerController�� ����� ����Ǿ����� Ȯ��
        if (reward1 != null)
        {
            Debug.Log("Reward item created successfully.");

            // ���� PlayerController�� �����۰� �浹�� ���¿��� ����� ���, �׿��� ������ �ٽ� üũ
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
