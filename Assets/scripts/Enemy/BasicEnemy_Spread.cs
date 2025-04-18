using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy_Spread: Enemy
{
    public float waveAmplitude = 0.5f;  // 파동의 크기
    public float waveFrequency = 1f;    // 파동의 주기

    public override void shoot()
    {
        if (isBulletBlocked) return;
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            // 총알 생성
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.transform.localScale = new Vector3(bulletsize, bulletsize, bulletsize);

            // 파동 효과를 위한 X축 위치 계산
            float waveOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

            // Rigidbody2D를 통해 총알 속도 설정
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            if (rbBullet != null)
            {
                rbBullet.linearVelocity = new Vector2(waveOffset, -1) * bulletSpeed;  // X축 파동 + Y축 아래로 발사
            }
        }
    }
    public override void move()
    {
        if (transform.position.y > 4f)
        {
            // Y축으로 내려오기
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    void Update()
    {
        if (Hp > 0) {
            shoot();
            move();
        }
        
    }
}
