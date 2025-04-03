using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {
        // 게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // 에디터에서 실행 중일 때 게임을 종료
#else
            Application.Quit();  // 빌드된 게임에서 게임 종료
#endif
    }
}
