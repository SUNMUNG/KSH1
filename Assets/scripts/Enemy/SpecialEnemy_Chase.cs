using UnityEngine;

public class SpecialEnemy_Chase : Enemy
{
        public GameObject player;  // 플레이어 객체를 참조
        public float spreadAngle = 10f; // 총알의 불규칙성 범위

        public override void shoot()
        {
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
        if(Hp> 0)
        {
            shoot();
        }
    }
}

        

    
