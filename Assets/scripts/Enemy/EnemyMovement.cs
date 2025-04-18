using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum MovementPattern
    {
        Horizontal,  // ���ʿ��� ���������� �̵�
        Vertical,    // ������ �Ʒ��� �̵�
        Diagonal,    // �밢�� �������� �̵�
        Circular     // ���� ����
    }

    public MovementPattern movementPattern;  // �̵� ����
    public float speed = 2f;  // �̵� �ӵ�
    public float radius = 5f;  // ���� ������ ���� ������

    private float timer = 0f;  // �ð� ������� ������ �����ϴ� Ÿ�̸�

    void Update()
    {
        switch (movementPattern)
        {
            case MovementPattern.Horizontal:
                MoveHorizontally();
                break;
            case MovementPattern.Vertical:
                MoveVertically();
                break;
            case MovementPattern.Diagonal:
                MoveDiagonally();
                break;
            case MovementPattern.Circular:
                MoveInCircle();
                break;
        }
    }

    // ���� �̵�
    void MoveHorizontally()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    // ���� �̵�
    void MoveVertically()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    // �밢�� �̵�
    void MoveDiagonally()
    {
        transform.position += new Vector3(1, -1, 0) * speed * Time.deltaTime;
    }

    // ���� �������� �̵�
    void MoveInCircle()
    {
        timer += Time.deltaTime;
        float x = Mathf.Cos(timer * speed) * radius;
        float y = Mathf.Sin(timer * speed) * radius;
        transform.position = new Vector3(x, y, 0);
    }
}
