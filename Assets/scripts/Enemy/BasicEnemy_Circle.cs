using UnityEngine;

public class BasicEnemy_Circle : Enemy
{

    public int bulletCount = 20;  // 한 번에 발사할 탄환의 개수
    public float radius = 5f;     // 탄환이 퍼지는 원의 반지름
    public float angleStep;       // 각도 간격
    public float randomAngleOffset = 10f; // 각도에 랜덤 오프셋을 주어 매번 다른 위치에서 발사

    private void Start()
    {
        base.Start();
        angleStep = 360f / bulletCount;  // 각도를 일정하게 나누기
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
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;  // 주기적으로 발사

            for (int i = 0; i < bulletCount; i++)
            {
                // 원형으로 발사할 각도를 계산
                float angle = angleStep * i;

                // 각도에 랜덤 오프셋을 추가하여 매번 다른 위치에서 발사되도록 설정
                float randomOffset = Random.Range(-randomAngleOffset, randomAngleOffset);
                angle += randomOffset;

                // 각도를 라디안 값으로 변환
                float angleRad = angle * Mathf.Deg2Rad;

                // 탄환의 방향을 계산
                float x = Mathf.Cos(angleRad) * radius;
                float y = Mathf.Sin(angleRad) * radius;

                // 탄환의 방향을 설정
                Vector3 bulletDirection = new Vector3(x, y, 0).normalized;

                // 탄환을 생성하고 발사
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = bulletDirection * bulletSpeed;
            }
        }
    }
}
