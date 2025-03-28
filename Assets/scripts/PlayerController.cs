using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float atkpt = 1f;
    private float speed = 8f;
    public float hp = 999f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private float bulletSpeed = 10f;
    private float fireRate = 0.1f;
    private float nextFireTime = 0f;
   


    private Animator animator;
    private Rigidbody2D Rigidbody;
    private Collider2D Collider;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Collider = GetComponent<Collider2D>();    
        SetCamPos();
    }

    void Update()
    {
        PlayerMove();
        ShootBullet();
    }

    public float HPText()
    {
        return hp;
    }
    void PlayerMove()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // 이동 방향 설정
        Vector2 moveDir = new Vector2(moveX, moveY).normalized * speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Rigidbody.linearVelocity = moveDir*0.3f;
        }
        else { Rigidbody.linearVelocity = moveDir; }
       

        

        // 화면 경계 제한
        ClampPos();
    }

    void ShootBullet()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            rbBullet.linearVelocity = Vector2.up * bulletSpeed;
           
        }
    }
    public void damaged(float damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            animator.SetBool("isdie", true);
            if (Collider != null)
                Collider.enabled = false;
            speed = 0f;
            Destroy(gameObject, 1f);
            Debug.Log("게임 오버");
        }
    }

    void ClampPos()
    {
        // Rigidbody 사용 시에도 위치 제한 필요
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void SetCamPos()
    {
        Camera cam = Camera.main;
        minBounds = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxBounds = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        minBounds.y += 0.2f;
        minBounds.x += 0.2f;
        maxBounds.x -= 0.2f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "border")
        {
            ClampPos();
        }
        
    }
}