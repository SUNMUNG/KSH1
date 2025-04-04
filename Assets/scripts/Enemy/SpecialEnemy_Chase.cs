using UnityEngine;

public class SpecialEnemy_Chase : Enemy
{
        GameObject player;  // 플레이어 객체를 참조
        public float spreadAngle = 10f; // 총알의 불규칙성 범위

    private float randomYPosition;
    private float randomZigzagSpeed;
    private float randomMinX;
    private float randomMaxX;

    public override void move()
    {
        // Y축만 이동시키고 X축은 고정
        if (transform.position.y > randomYPosition) // Y축이 랜덤 값에 도달하면 멈추고 지그재그 시작
        {
            // Y축으로 내려오기
            transform.position += (Vector3)Vector2.down * speed * Time.deltaTime;
        }
        else
        {
            // Y좌표가 랜덤 값에 도달했으면 지그재그 움직임 시작
            // Sin 함수로 좌우로 움직이도록 설정
            float zigzagMovement = Mathf.Sin(Time.time * randomZigzagSpeed); // -1에서 1 사이의 값

            // 지그재그 범위를 minX에서 maxX로 설정
            float newX = Mathf.Lerp(randomMinX, randomMaxX, (zigzagMovement + 1f) / 2f); // -1~1 -> 0~1 범위로 변환 후 Lerp

            // 현재 Y축 위치를 유지하고, X축을 지그재그로 이동
            transform.position = new Vector2(newX, transform.position.y);
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
                int bulletCount = 3;

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

        // 초기 랜덤 값 설정
        randomYPosition = Random.Range(3f, 4f); // Y 좌표 랜덤화
        randomMinX = Random.Range(-7f, -3f);  // X 최소값 랜덤화
        randomMaxX = Random.Range(3f, 7f);    // X 최대값 랜덤화
        randomZigzagSpeed = Random.Range(0.1f, 0.3f);
        if (Hp> 0)
        {
            move();
            shoot();
        }
    }
}

        

    
