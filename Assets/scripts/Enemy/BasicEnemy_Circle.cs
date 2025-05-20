using UnityEngine;

public class BasicEnemy_Circle : Enemy
{
    public int bulletCount = 20;  // 한 번 발사할 총알 개수
    public float radius_c = 5f;   // 원형 패턴의 반지름 크기
    public float angleStep;       // 각도 간격
    public float randomAngleOffset = 10f; // 각도에 랜덤값을 더해 약간 다른 위치에서 발사

    private new void Start()
    {
        base.Start();
        angleStep = 360f / bulletCount;  // 각도를 균등하게 나누기
    }

    private void Update()
    {
        if (Hp > 0)
        {
            shoot();
        }
    }

    public override void shoot()
    {
        if (isBulletBlocked) return;
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;  // 다음 발사 시간 설정

            for (int i = 0; i < bulletCount; i++)
            {
                // 기본 발사 각도 계산
                float angle = angleStep * i;

                // 각도에 랜덤값을 더해 약간 다른 위치에서 발사되도록 함
                float randomOffset = Random.Range(-randomAngleOffset, randomAngleOffset);
                angle += randomOffset;

                // 각도를 라디안으로 변환
                float angleRad = angle * Mathf.Deg2Rad;

                // 원형 패턴의 좌표 계산
                float x = Mathf.Cos(angleRad) * radius_c;
                float y = Mathf.Sin(angleRad) * radius_c;

                // 원형 패턴의 방향 설정
                Vector3 bulletDirection = new Vector3(x, y, 0).normalized;

                // 원형 패턴으로 발사
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                bullet.transform.localScale = new Vector3(bulletsize, bulletsize, bulletsize);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = bulletDirection * bulletSpeed;
            }
        }
    }
}
