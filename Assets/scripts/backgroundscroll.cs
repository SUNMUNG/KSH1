using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.1f;  // ��ũ�� �ӵ� ����
    private Material mat;
    private Vector2 offset;

    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material; // ����� Material ��������
        offset = mat.mainTextureOffset;
    }

    void Update()
    {
        offset.y += scrollSpeed * Time.deltaTime; // �Ʒ��� ��ũ��
        mat.mainTextureOffset = offset;
    }
}
