using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum MovementPattern
    {
        Horizontal,  // 왼쪽에서 오른쪽으로 이동
        Vertical,    // 위에서 아래로 이동
        Diagonal,    // 대각선 방향으로 이동
        Circular     // 원형 패턴
    }

    public MovementPattern movementPattern;  // 이동 패턴
    public float speed = 2f;  // 이동 속도
    public float radius = 5f;  // 원형 패턴을 위한 반지름

    private float timer = 0f;  // 시간 기반으로 패턴을 제어하는 타이머

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

    // 수평 이동
    void MoveHorizontally()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    // 수직 이동
    void MoveVertically()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    // 대각선 이동
    void MoveDiagonally()
    {
        transform.position += new Vector3(1, -1, 0) * speed * Time.deltaTime;
    }

    // 원형 패턴으로 이동
    void MoveInCircle()
    {
        timer += Time.deltaTime;
        float x = Mathf.Cos(timer * speed) * radius;
        float y = Mathf.Sin(timer * speed) * radius;
        transform.position = new Vector3(x, y, 0);
    }
}
