using UnityEngine;

public class SpecialEnemy_Chase : Enemy
{
        public GameObject player;  // �÷��̾� ��ü�� ����
        public float spreadAngle = 10f; // �Ѿ��� �ұ�Ģ�� ����

        public override void shoot()
        {
            if (Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;  // �߻� �� ���� �߻� �ð� ����

                // �÷��̾� ��ġ ����
                Vector3 playerPosition = player.transform.position;

                // �Ѿ� �߻� (�μ� ���� �Ѿ� �߻�)
                int bulletCount = 3;

                for (int i = 0; i < bulletCount; i++)
                {
                    // �ұ�Ģ�� ���� ��� (���� -spreadAngle to +spreadAngle)
                    float randomAngle = Random.Range(-spreadAngle, spreadAngle);

                    // �÷��̾� �������� ���� ���
                    Vector2 direction = (playerPosition - transform.position).normalized;

                    // �ұ�Ģ�� ������ �����Ͽ� ���� ����
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + randomAngle;

                    // ���� ���� ���
                    Vector2 shootDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));

                    // �Ѿ� �߻�
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                    // �Ѿ��� �ӵ� ����
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

        

    
