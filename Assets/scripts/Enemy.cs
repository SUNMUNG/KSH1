using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Hp=5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 3f;
    public float fireRate = 0.3f;
    public float speed = 1f;
    public float nextFireTime = 0f;


    private Animator animator;
    private Collider2D enemyCollider;
    void Start()
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

            animator.SetBool("isdie",true); //애니메이션 트리거
            Destroy(gameObject,1f); //enemy 게임오브젝트 제거;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bulletborder")
        {
            Destroy(gameObject);
        }
    }
    public virtual void shoot() { }

    


}
