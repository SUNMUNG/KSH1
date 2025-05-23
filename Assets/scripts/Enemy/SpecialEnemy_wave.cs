using UnityEngine;

public class SpecialEnemy_wave : Enemy
{
    public float waveFrequency = 0.5f;   // �ĵ� �ֱ� (�ð��� �߻� ��)
    public int bulletsPerWave = 8;        // �� �ĵ����� �߻��� �Ѿ��� �� (���� ��ο� ����)
    public float waveRadius = 1f;         // ���� ����� ������       
    public float rotationSpeed = 30f;     // ���� ������ ȸ�� �ӵ� (��/��)
    private float waveTimer = 0f;         // �ĵ� Ÿ�̸�
    private float currentRotation = 0f;   // ���� ����� ���� ȸ�� ����

    public override void shoot()
    {
        if (isBulletBlocked) return;
        // �ĵ� Ÿ�̸� ����
        waveTimer += Time.deltaTime;

        if (waveTimer >= waveFrequency)
        {
            waveTimer = 0f;

            // ���� ��� ȸ�� �߰�
            currentRotation += rotationSpeed * Time.deltaTime;

            for (int i = 0; i < bulletsPerWave; i++)
            {
                // ���� ���: ���� ��θ� ���� ������ ����� �Ѿ��� �߻�
                float angle = i * (360f / bulletsPerWave);  // ���� ���� ���
                float angleInRadians = (angle + currentRotation) * Mathf.Deg2Rad; // ȸ������ �߰��Ͽ� �Ѿ��� ���� ����

                // ���� ����� X, Y ��ǥ ��� (�������� �߻�)
                float xOffset = Mathf.Cos(angleInRadians) * waveRadius;
                float yOffset = Mathf.Sin(angleInRadians) * waveRadius;

                // �Ѿ��� �߻� ��ġ�� ���� ��ο� �°� ����
                Vector3 spawnPosition = new Vector3(firePoint.position.x + xOffset, firePoint.position.y + yOffset, firePoint.position.z);

                // �Ѿ� ����
                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
                bullet.transform.localScale = new Vector3(bulletsize, bulletsize, bulletsize);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    // ���� ��η� �߻�� ����
                    Vector2 direction = new Vector2(xOffset, yOffset).normalized;

                    // �Ѿ� �ӵ� ���� (���� + �ӵ�)
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
