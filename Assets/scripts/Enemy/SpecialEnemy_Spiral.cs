using UnityEngine;

public class SpecialEnemy_Spiral : Enemy
{
    public float angleIncrement = 10f; // ���� ������
    public int numberOfBullets = 30;   // �߻��� �Ѿ��� ��
    private float currentAngle = 0f;
    public float randomAngleVariance = 5f;  // ������ ���� ��ȭ�� �� ���� (����)
    public float randomSpeedVariance = 0.5f; // �ӵ��� ���� ��ȭ�� �� ���� (����)

    public override void shoot()
    {
        if (isBulletBlocked) return;
        for (int i = 0; i < numberOfBullets; i++)
        {
            // ������ ���� �߰� (���� ������ ��������ŭ ��ȭ�� ��)
            float randomAngle = Random.Range(-randomAngleVariance, randomAngleVariance);
            float angle = currentAngle + (i * angleIncrement) + randomAngle;

            // �Ѿ� ����
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            bullet.transform.localScale = new Vector3(bulletsize, bulletsize, bulletsize);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // �ӵ��� ������ ��ȭ�� �߰�
                float randomSpeed = Random.Range(-randomSpeedVariance, randomSpeedVariance);
                rb.linearVelocity = bullet.transform.up * (bulletSpeed + randomSpeed);
            }
        }

        // �������� ��� �����ϱ� ���� ���� ������Ʈ
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
