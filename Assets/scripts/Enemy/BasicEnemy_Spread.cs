using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy_Spread: Enemy
{
    public float waveAmplitude = 0.5f;  // �ĵ��� ũ��
    public float waveFrequency = 1f;    // �ĵ��� �ֱ�

    public override void shoot()
    {
        if (isBulletBlocked) return;
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            // �Ѿ� ����
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.transform.localScale = new Vector3(bulletsize, bulletsize, bulletsize);

            // �ĵ� ȿ���� ���� X�� ��ġ ���
            float waveOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

            // Rigidbody2D�� ���� �Ѿ� �ӵ� ����
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            if (rbBullet != null)
            {
                rbBullet.linearVelocity = new Vector2(waveOffset, -1) * bulletSpeed;  // X�� �ĵ� + Y�� �Ʒ��� �߻�
            }
        }
    }
    public override void move()
    {
        if (transform.position.y > 4f)
        {
            // Y������ ��������
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
