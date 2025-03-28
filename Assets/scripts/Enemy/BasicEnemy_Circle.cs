using UnityEngine;

public class BasicEnemy_Circle : Enemy
{

    public int bulletCount = 20;  // �� ���� �߻��� źȯ�� ����
    public float radius = 5f;     // źȯ�� ������ ���� ������
    public float angleStep;       // ���� ����
    public float randomAngleOffset = 10f; // ������ ���� �������� �־� �Ź� �ٸ� ��ġ���� �߻�

    private void Start()
    {
        base.Start();
        angleStep = 360f / bulletCount;  // ������ �����ϰ� ������
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
            nextFireTime = Time.time + fireRate;  // �ֱ������� �߻�

            for (int i = 0; i < bulletCount; i++)
            {
                // �������� �߻��� ������ ���
                float angle = angleStep * i;

                // ������ ���� �������� �߰��Ͽ� �Ź� �ٸ� ��ġ���� �߻�ǵ��� ����
                float randomOffset = Random.Range(-randomAngleOffset, randomAngleOffset);
                angle += randomOffset;

                // ������ ���� ������ ��ȯ
                float angleRad = angle * Mathf.Deg2Rad;

                // źȯ�� ������ ���
                float x = Mathf.Cos(angleRad) * radius;
                float y = Mathf.Sin(angleRad) * radius;

                // źȯ�� ������ ����
                Vector3 bulletDirection = new Vector3(x, y, 0).normalized;

                // źȯ�� �����ϰ� �߻�
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = bulletDirection * bulletSpeed;
            }
        }
    }
}
