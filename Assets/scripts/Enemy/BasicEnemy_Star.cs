using UnityEngine;

public class BasicEnemy_Star : Enemy
{
    public int numBullets = 8;  // 발사할 총알 개수
    public float angleStep = 45f; // 각도 간격 (360도 / numBullets)
    public float randomAngleVariation = 10f; // 각도 랜덤 변형 범위 (±)

    

    public override void move()
    {
        // Y 위치가 4에 도달할 때까지 내려옵니다.
        if (transform.position.y > 4f)
        {
            // Y축으로 내려오기
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }
    public override void shoot()
    {
        if (isBulletBlocked) return;

        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            for (int i = 0; i < numBullets; i++)
            {
                // 원래의 각도에 랜덤 값을 추가
                float angle = i * angleStep + Random.Range(-randomAngleVariation, randomAngleVariation);

                Vector2 direction = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));

                // 총알 생성
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                bullet.transform.localScale = new Vector3(bulletsize,bulletsize,bulletsize);

                // Rigidbody2D를 통해 총알 속도 설정
                Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
                if (rbBullet != null)
                {
                    rbBullet.linearVelocity = direction * bulletSpeed;  // 방향에 맞게 발사
                }
            }
        }
    }

    void Update()
    {
        if (Hp > 0)
        {
            shoot();
            move();
        }
    }
}
