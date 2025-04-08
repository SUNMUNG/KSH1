using UnityEngine;

public class SpecialEnemy_Chase : Enemy
{
    GameObject player;  // 플레이어 객체를 참조
    public float spreadAngle = 10f; // 총알의 불규칙성 범위
    private bool movingRight = true; // 이동 방향 (오른쪽으로 이동할지 왼쪽으로 이동할지)
    private float moveBoundaryLeft = -7f; // 왼쪽 경계
    private float moveBoundaryRight = 7f; //오른쪽 경계



    public override void move()
    {
        // Y 위치가 4에 도달할 때까지 내려옵니다.
        if (transform.position.y > 4f)
        {
            // Y축으로 내려오기
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else
        {
            // Y축이 4에 도달하면 좌우 이동 시작
            if (movingRight)
            {
                // 오른쪽으로 이동
                transform.position += Vector3.right * speed * Time.deltaTime;

                // 경계에 닿으면 반대 방향으로 이동
                if (transform.position.x >= moveBoundaryRight)
                {
                    movingRight = false;  // 왼쪽으로 이동
                }
            }
            else
            {
                // 왼쪽으로 이동
                transform.position += Vector3.left * speed * Time.deltaTime;

                // 경계에 닿으면 반대 방향으로 이동
                if (transform.position.x <= moveBoundaryLeft)
                {
                    movingRight = true;  // 오른쪽으로 이동
                }
            }
        }
    }
    public override void shoot()
    {
        player = GameObject.Find("Player");
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;  // 발사 후 다음 발사 시간 설정

            // 플레이어 위치 추적
            Vector3 playerPosition = player.transform.position;

            // 총알 발사 (두세 개의 총알 발사)
            int bulletCount = 2;

            for (int i = 0; i < bulletCount; i++)
            {
                // 불규칙한 각도 계산 (범위 -spreadAngle to +spreadAngle)
                float randomAngle = Random.Range(-spreadAngle, spreadAngle);

                // 플레이어 방향으로 각도 계산
                Vector2 direction = (playerPosition - transform.position).normalized;

                // 불규칙한 각도를 적용하여 방향 변경
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + randomAngle;

                // 방향 벡터 계산
                Vector2 shootDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));

                // 총알 발사
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                // 총알의 속도 적용
                rb.linearVelocity = shootDirection * bulletSpeed;
            }
        }
    }

    private void Update()
    {

        if (Hp > 0)
        {
            move();
            shoot();
        }
    }
}
