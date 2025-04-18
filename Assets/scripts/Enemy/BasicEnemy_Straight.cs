using UnityEngine;

public class BasicEnemy_Straight : Enemy
{

    public override void shoot()
    {
        if (isBulletBlocked) return;
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate * 3f;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.transform.localScale = new Vector3(bulletsize, bulletsize, bulletsize);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            rbBullet.linearVelocity = Vector2.down * bulletSpeed;
        }
    }

    public override void move()
    {
        // Y축만 이동시키고 X축은 고정
        transform.position += (Vector3)Vector2.down * speed * Time.deltaTime;

    }


    void Update()
    {
        if(Hp > 0)
        {
            shoot();
            move();
        }
       
    }
}
