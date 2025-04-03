using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int power = 0;
    private float speed = 8f;
    public float hp = 999f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private float bulletSpeed = 10f;
    private float fireRate = 0.1f;
    private float nextFireTime = 0f;
    private Coroutine powerUpCoroutine;


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

    public float HPOut()
    {
        return hp;
    }

    public void PowerUp(int duration)
    {
        // power가 이미 증가한 상태라면 다시 시작하지 않도록 방지
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine); // 기존 코루틴 중단
            Debug.Log("기존 파워업 코루틴 중단됨.");
        }

        // power 증가
        power++;
        Debug.Log("Power increased! Current power: " + power);

        // 파워업 지속 시간을 관리하는 코루틴 시작
        powerUpCoroutine = StartCoroutine(PowerUpDuration(duration));
    }

    private IEnumerator PowerUpDuration(int duration)
    {
        Debug.Log("PowerUp 시작. 지속 시간: " + duration + "초");

        yield return new WaitForSeconds(duration); // 주어진 시간만큼 기다림

        // 시간이 끝나면 power 초기화
        power = 0;
        Debug.Log("PowerUp 종료. Power 초기화됨. Current power: " + power);
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
            switch (power)
            {
                case 0:
                    // 기본 총알 (1개만 발사)
                    nextFireTime = Time.time + fireRate;
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 90));
                    Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
                    rbBullet.linearVelocity = Vector2.up * bulletSpeed;
                    break;

                case 1:
                    // power 1: 총알 3개 발사 (중앙 + 양옆)
                    nextFireTime = Time.time + fireRate;

                    // 중앙 총알 (직선 발사)
                    GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 90));
                    Rigidbody2D rbBullet1 = bullet1.GetComponent<Rigidbody2D>();
                    rbBullet1.linearVelocity = Vector2.up * bulletSpeed;

                    // 왼쪽 총알 (직선 발사)
                    GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 90));
                    Rigidbody2D rbBullet2 = bullet2.GetComponent<Rigidbody2D>();
                    rbBullet2.linearVelocity = Vector2.up * bulletSpeed;

                    // 오른쪽 총알 (직선 발사)
                    GameObject bullet3 = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 90));
                    Rigidbody2D rbBullet3 = bullet3.GetComponent<Rigidbody2D>();
                    rbBullet3.linearVelocity = Vector2.up * bulletSpeed;
                    break;

                case 2:
                    // power 2: 총알 5개 발사 (중앙 3개 + 양끝에 1개씩 앞쪽에 추가)
                    nextFireTime = Time.time + fireRate;

                    // 중앙 총알 (직선 발사)
                    GameObject bullet4 = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 90));
                    Rigidbody2D rbBullet4 = bullet4.GetComponent<Rigidbody2D>();
                    rbBullet4.linearVelocity = Vector2.up * bulletSpeed;

                    // 왼쪽 총알 (사선 발사)
                    GameObject bullet5 = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 90));
                    Rigidbody2D rbBullet5 = bullet5.GetComponent<Rigidbody2D>();
                    rbBullet5.linearVelocity = new Vector2(-0.5f, 1f).normalized * bulletSpeed;

                    // 오른쪽 총알 (사선 발사)
                    GameObject bullet6 = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 90));
                    Rigidbody2D rbBullet6 = bullet6.GetComponent<Rigidbody2D>();
                    rbBullet6.linearVelocity = new Vector2(0.5f, 1f).normalized * bulletSpeed;

                    // 왼쪽 끝 앞쪽 총알 (사선 발사)
                    GameObject bullet7 = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 90));
                    Rigidbody2D rbBullet7 = bullet7.GetComponent<Rigidbody2D>();
                    rbBullet7.linearVelocity = new Vector2(-1f, 1.5f).normalized * bulletSpeed;

                    // 오른쪽 끝 앞쪽 총알 (사선 발사)
                    GameObject bullet8 = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 90));
                    Rigidbody2D rbBullet8 = bullet8.GetComponent<Rigidbody2D>();
                    rbBullet8.linearVelocity = new Vector2(1f, 1.5f).normalized * bulletSpeed;
                    break;
            }
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