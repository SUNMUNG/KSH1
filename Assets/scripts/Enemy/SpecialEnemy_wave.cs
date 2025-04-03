using UnityEngine;

public class SpecialEnemy_wave : Enemy
{
    public float waveFrequency = 0.5f;   // 파동 주기 (시간당 발사 빈도)
    public int bulletsPerWave = 8;        // 각 파동에서 발사할 총알의 수 (원형 경로에 따라)
    public float waveRadius = 1f;         // 원형 경로의 반지름       
    public float rotationSpeed = 30f;     // 원형 패턴의 회전 속도 (도/초)
    private float waveTimer = 0f;         // 파동 타이머
    private float currentRotation = 0f;   // 원형 경로의 현재 회전 각도

    public override void shoot()
    {
        // 파동 타이머 갱신
        waveTimer += Time.deltaTime;

        if (waveTimer >= waveFrequency)
        {
            waveTimer = 0f;

            // 원형 경로 회전 추가
            currentRotation += rotationSpeed * Time.deltaTime;

            for (int i = 0; i < bulletsPerWave; i++)
            {
                // 각도 계산: 원형 경로를 따라서 각도를 나누어서 총알을 발사
                float angle = i * (360f / bulletsPerWave);  // 각도 간격 계산
                float angleInRadians = (angle + currentRotation) * Mathf.Deg2Rad; // 회전값을 추가하여 총알의 각도 변경

                // 원형 경로의 X, Y 좌표 계산 (원형으로 발사)
                float xOffset = Mathf.Cos(angleInRadians) * waveRadius;
                float yOffset = Mathf.Sin(angleInRadians) * waveRadius;

                // 총알의 발사 위치를 원형 경로에 맞게 설정
                Vector3 spawnPosition = new Vector3(firePoint.position.x + xOffset, firePoint.position.y + yOffset, firePoint.position.z);

                // 총알 생성
                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    // 원형 경로로 발사될 방향
                    Vector2 direction = new Vector2(xOffset, yOffset).normalized;

                    // 총알 속도 적용 (방향 + 속도)
                    rb.linearVelocity = direction * bulletSpeed;
                }   
            }
        }
    }

    void EnemyMove()
    {
        transform.Translate(Vector3.right * speed * moveDirection * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "border")
        {
            Debug.Log(collision.gameObject.tag);
            moveDirection *= -1;
        }
    }
    private void Update()
    {
        if (Hp > 0)
        {
            shoot();
            EnemyMove();
        }
    }
}
