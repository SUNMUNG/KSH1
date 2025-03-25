using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.1f;  // 스크롤 속도 조절
    private Material mat;
    private Vector2 offset;

    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material; // 배경의 Material 가져오기
        offset = mat.mainTextureOffset;
    }

    void Update()
    {
        offset.y += scrollSpeed * Time.deltaTime; // 아래로 스크롤
        mat.mainTextureOffset = offset;
    }
}
