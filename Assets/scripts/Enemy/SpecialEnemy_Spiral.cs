using UnityEngine;

public class SpecialEnemy_Spiral : Enemy
{
    public float angleIncrement = 10f; // 각도 증가량
    public int numberOfBullets = 30;   // 발사할 총알의 수
    private float currentAngle = 0f;
    public float randomAngleVariance = 5f;  // 각도에 랜덤 변화를 줄 범위 (±값)
    public float randomSpeedVariance = 0.5f; // 속도에 랜덤 변화를 줄 범위 (±값)

    public override void shoot()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            // 랜덤한 각도 추가 (원래 각도에 ±범위만큼 변화를 줌)
            float randomAngle = Random.Range(-randomAngleVariance, randomAngleVariance);
            float angle = currentAngle + (i * angleIncrement) + randomAngle;

            // 총알 생성
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // 속도에 랜덤한 변화를 추가
                float randomSpeed = Random.Range(-randomSpeedVariance, randomSpeedVariance);
                rb.linearVelocity = bullet.transform.up * (bulletSpeed + randomSpeed);
            }
        }

        // 나선형을 계속 진행하기 위해 각도 업데이트
        currentAngle += angleIncrement;
    }
    private void Update()
    {
        if(Hp > 0)
        {
            shoot();
        }
    }
}
