using UnityEngine;

public class SpecialEnemy_Chase : Enemy
{
        GameObject player;  // �÷��̾� ��ü�� ����
        public float spreadAngle = 10f; // �Ѿ��� �ұ�Ģ�� ����

    private float randomYPosition;
    private float randomZigzagSpeed;
    private float randomMinX;
    private float randomMaxX;

    public override void move()
    {
        // Y�ุ �̵���Ű�� X���� ����
        if (transform.position.y > randomYPosition) // Y���� ���� ���� �����ϸ� ���߰� ������� ����
        {
            // Y������ ��������
            transform.position += (Vector3)Vector2.down * speed * Time.deltaTime;
        }
        else
        {
            // Y��ǥ�� ���� ���� ���������� ������� ������ ����
            // Sin �Լ��� �¿�� �����̵��� ����
            float zigzagMovement = Mathf.Sin(Time.time * randomZigzagSpeed); // -1���� 1 ������ ��

            // ������� ������ minX���� maxX�� ����
            float newX = Mathf.Lerp(randomMinX, randomMaxX, (zigzagMovement + 1f) / 2f); // -1~1 -> 0~1 ������ ��ȯ �� Lerp

            // ���� Y�� ��ġ�� �����ϰ�, X���� ������׷� �̵�
            transform.position = new Vector2(newX, transform.position.y);
        }
    }

    public override void shoot()
        {
        player = GameObject.Find("Player");
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

        // �ʱ� ���� �� ����
        randomYPosition = Random.Range(3f, 4f); // Y ��ǥ ����ȭ
        randomMinX = Random.Range(-7f, -3f);  // X �ּҰ� ����ȭ
        randomMaxX = Random.Range(3f, 7f);    // X �ִ밪 ����ȭ
        randomZigzagSpeed = Random.Range(0.1f, 0.3f);
        if (Hp> 0)
        {
            move();
            shoot();
        }
    }
}

        

    
