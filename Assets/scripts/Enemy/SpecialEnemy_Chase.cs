using UnityEngine;

public class SpecialEnemy_Chase : Enemy
{
    GameObject player;  // �÷��̾� ��ü�� ����
    public float spreadAngle = 10f; // �Ѿ��� �ұ�Ģ�� ����
    private bool movingRight = true; // �̵� ���� (���������� �̵����� �������� �̵�����)
    private float moveBoundaryLeft = -7f; // ���� ���
    private float moveBoundaryRight = 7f; //������ ���



    public override void move()
    {
        // Y ��ġ�� 4�� ������ ������ �����ɴϴ�.
        if (transform.position.y > 4f)
        {
            // Y������ ��������
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else
        {
            // Y���� 4�� �����ϸ� �¿� �̵� ����
            if (movingRight)
            {
                // ���������� �̵�
                transform.position += Vector3.right * speed * Time.deltaTime;

                // ��迡 ������ �ݴ� �������� �̵�
                if (transform.position.x >= moveBoundaryRight)
                {
                    movingRight = false;  // �������� �̵�
                }
            }
            else
            {
                // �������� �̵�
                transform.position += Vector3.left * speed * Time.deltaTime;

                // ��迡 ������ �ݴ� �������� �̵�
                if (transform.position.x <= moveBoundaryLeft)
                {
                    movingRight = true;  // ���������� �̵�
                }
            }
        }
    }
    public override void shoot()
    {
        if (isBulletBlocked) return;
        player = GameObject.Find("Player");
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;  // �߻� �� ���� �߻� �ð� ����

            // �÷��̾� ��ġ ����
            Vector3 playerPosition = player.transform.position;

            // �Ѿ� �߻� (�μ� ���� �Ѿ� �߻�)
            int bulletCount = 1;

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
                bullet.transform.localScale = new Vector3(bulletsize, bulletsize, bulletsize);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                // �Ѿ��� �ӵ� ����
                rb.linearVelocity = shootDirection * bulletSpeed;
            }
        }
    }

    private void Update()
    {

        if (Hp > 0)
        {
            move();
            shoot();
        }
    }
}
