using UnityEngine;

public class BasicEnemy_Straight : Enemy
{

    public override void shoot()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate * 3f;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            rbBullet.linearVelocity = Vector2.down * bulletSpeed;
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
