using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int power = 0;
    private float speed = 7f;
    public float hp = 6f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private float bulletSpeed = 10f;
    private float fireRate = 0.1f;
    private float nextFireTime = 0f;
    private Coroutine powerUpCoroutine;

    private SpriteRenderer spriteRenderer;
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
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        
        if (power >= 2)
        {
            Debug.Log("Power is already at maximum. No power-up applied.");
            return;
        }


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
            Rigidbody.linearVelocity = moveDir * 0.3f;
        }
        else { Rigidbody.linearVelocity = moveDir; }




        // 화면 경계 제한
        ClampPos();
    }

    void ShootBullet()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // 한번만 갱신되도록 위치 변경

            switch (power)
            {
                case 0:
                    ShootSingleBullet();
                    break;

                case 1:
                    ShootTripleBullet();
                    break;

                case 2:
                    ShootFiveBullets();
                    break;
            }
        }
    }

    void ShootSingleBullet()
    {
        // 기본 총알 (1개만 발사)
        SpawnBullet(firePoint.position, 90);
    }

    void ShootTripleBullet()
    {
        // power 1: 총알 3개 발사 (중앙 + 양옆)
        SpawnBullet(firePoint.position, 90); // 중앙 총알

        // 왼쪽 총알 (직선 발사)
        Vector3 leftPosition = firePoint.position + new Vector3(-0.25f, 0f, 0f);
        SpawnBullet(leftPosition, 90);

        // 오른쪽 총알 (직선 발사)
        Vector3 rightPosition = firePoint.position + new Vector3(0.25f, 0f, 0f);
        SpawnBullet(rightPosition, 90);
    }

    void ShootFiveBullets()
    {
        // power 2: 총알 5개 발사 (중앙 3개 + 양끝에 1개씩 앞쪽에 추가)
        SpawnBullet(firePoint.position, 90); // 중앙 총알

        // 왼쪽 총알 (사선 발사)
        SpawnBulletWithAngle(firePoint.position, new Vector2(-0.5f, 1f), bulletSpeed);

        // 오른쪽 총알 (사선 발사)
        SpawnBulletWithAngle(firePoint.position, new Vector2(0.5f, 1f), bulletSpeed);

        // 왼쪽 끝 앞쪽 총알 (사선 발사)
        SpawnBulletWithAngle(firePoint.position, new Vector2(-1f, 1.5f), bulletSpeed);

        // 오른쪽 끝 앞쪽 총알 (사선 발사)
        SpawnBulletWithAngle(firePoint.position, new Vector2(1f, 1.5f), bulletSpeed);
    }

    void SpawnBullet(Vector3 position, float angle)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.linearVelocity = Vector2.up * bulletSpeed;
    }

    void SpawnBulletWithAngle(Vector3 position, Vector2 direction, float speed)
    {
        Vector2 normalizedDirection = direction.normalized;
        float angle = Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.linearVelocity = normalizedDirection * speed;
    }

    public void damaged(float damage)
    {
        hp -= damage;
        if (Collider != null)
        {
            Collider.enabled = false;
            Debug.Log("피격확인 무적시간 시작");
            StartCoroutine(BlinkSprite(2f));
            StartCoroutine(EnableColliderAfterDelay(2f));
        }


        if (hp <= 0)
        {
            animator.SetBool("isdie", true);
            if (Collider != null)
                Collider.enabled = false;
            speed = 0f;
            Destroy(gameObject, 1f);
            Debug.Log("게임 오버");
        }
    }
    private IEnumerator EnableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // 지정된 시간만큼 대기
        Collider.enabled = true;        // 콜라이더를 다시 활성화
        Debug.Log("무적시간 종료");
    }
    private IEnumerator BlinkSprite(float duration)
    {
        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < duration)
        {
            spriteRenderer.enabled = isVisible; // 스프라이트 표시/숨기기
            isVisible = !isVisible; // 가시성 반전
            elapsedTime += 0.1f; // 0.1초마다 토글 (깜빡임 속도 조정 가능)
            yield return new WaitForSeconds(0.1f); // 0.1초 대기
        }

        spriteRenderer.enabled = true; // 마지막에는 스프라이트가 보이도록 설정
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