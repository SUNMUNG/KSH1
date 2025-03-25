using UnityEngine;

public class bullet : MonoBehaviour
{
    float player_bullet_damage = 1f;
    float enemy_bullet_damage = 1f;

 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bulletborder")
        {
            Destroy(gameObject); //���������� �Ѿ�����
        }
        else if (collision.gameObject.tag == "Enemy" && gameObject.tag!= "EnemyBullet")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>(); //Enemy ��ũ��Ʈ ��������
            enemy.damaged(player_bullet_damage);
            Destroy(gameObject);//ü�°��ҽ�Ű�� �Ѿ������� ����
        }
        else if (collision.gameObject.tag == "Player" && gameObject.tag != "playerbullet")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.damaged(enemy_bullet_damage);
            Destroy(gameObject);
        }
    }

}

