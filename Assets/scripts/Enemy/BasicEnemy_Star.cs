using UnityEngine;

public class BasicEnemy_Star : Enemy
{
    public int numBullets = 8;  // �߻��� �Ѿ� ����
    public float angleStep = 45f; // ���� ���� (360�� / numBullets)

    public override void shoot()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            for (int i = 0; i < numBullets; i++)
            {
                float angle = i * angleStep;
                Vector2 direction = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));

                // �Ѿ� ����
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

                // Rigidbody2D�� ���� �Ѿ� �ӵ� ����
                Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
                if (rbBullet != null)
                {
                    rbBullet.linearVelocity = direction * bulletSpeed;  // ���⿡ �°� �߻�
                }
            }
        }
    }

    void Update()
    {
        if(Hp > 0)
        {
            shoot();
        }
        
    }
}
