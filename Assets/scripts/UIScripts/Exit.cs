using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {
        // ���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ��� ���� ���� �� ������ ����
#else
            Application.Quit();  // ����� ���ӿ��� ���� ����
#endif
    }
}
