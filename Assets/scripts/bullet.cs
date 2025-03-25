using UnityEngine;

public class bullet : MonoBehaviour
{
    float player_bullet_damage = 1f;
    float enemy_bullet_damage = 1f;

 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bulletborder")
        {
            Destroy(gameObject); //벽에닿으면 총알제거
        }
        else if (collision.gameObject.tag == "Enemy" && gameObject.tag!= "EnemyBullet")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>(); //Enemy 스크립트 가져오기
            enemy.damaged(player_bullet_damage);
            Destroy(gameObject);//체력감소시키고 총알프리팹 제거
        }
        else if (collision.gameObject.tag == "Player" && gameObject.tag != "playerbullet")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.damaged(enemy_bullet_damage);
            Destroy(gameObject);
        }
    }

}

