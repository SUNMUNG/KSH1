using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject stageMenu;
    public Image[] heartImages;  // 하트 이미지들을 담을 배열
    public Sprite fullHeart;     // 꽉 찬 하트 이미지
    public Sprite emptyHeart;    // 빈 하트 이미지
    public PlayerController playerController;  // PlayerController를 참조
    public void ExitGame()
    {
        // 게임 종료

        EditorApplication.isPlaying = false;  // 에디터에서 실행 중일 때 게임을 종료

            Application.Quit();  // 빌드된 게임에서 게임 종료

    }
    public void Returntomenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayStageStart(string name)
    {
        Debug.Log(name + "을 불러옵니다.");
        SceneManager.LoadScene(name);
    }
    public void StageReturn(Button button)
    {
        string Stage_name = button.name;

        PlayStageStart(Stage_name);
    }
    public void SettingMenuOpen()
    {
        SceneManager.LoadScene("SettingMenu");
    }
    public void StageMenuOpen()
    {

        if (stageMenu != null)
        {
            stageMenu.SetActive(true);
        }
        else
        {
            Debug.LogWarning("StageMenu 오브젝트를 찾을 수 없습니다!");
        }
    }

    public void PauseGame()
    {
        Debug.Log("게임을 일시중지합니다");
        Time.timeScale = 0f;
       // EditorApplication.isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("게임을 재개합니다.");
    }
    public void StageMenuClose()
    {

        if (stageMenu != null)
        {
            stageMenu.SetActive(false);
        }
        else
        {
            Debug.LogWarning("StageMenu 오브젝트를 찾을 수 없습니다!");
        }
    }
    private void Start()
    {
        // 초기 HP 값을 기반으로 하트 업데이트
        UpdateHearts();
    }

    private void Update()
    {
        // HP 값이 변경될 때마다 하트를 업데이트
        UpdateHearts();
    }

    // 하트 이미지를 현재 HP에 맞게 업데이트하는 메소드
    private void UpdateHearts()
    {
        if (playerController == null) return;  // PlayerController가 없으면 업데이트하지 않음

        float currentHP = playerController.hp;  // PlayerController에서 HP 값 가져오기

        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHP)
            {
                heartImages[i].sprite = fullHeart;  // 꽉 찬 하트
            }
            else
            {
                heartImages[i].sprite = emptyHeart;  // 빈 하트
            }
        }
    }
}
