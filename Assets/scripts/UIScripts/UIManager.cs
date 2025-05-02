using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography.X509Certificates;

public class UIManager : MonoBehaviour
{
    public GameObject stageMenu;
    public GameObject settingMenu;
    public Image[] heartImages;
    public Image[] starImages;
    public Image[] ultImages;
    public Sprite fullUlt;
    public Sprite emptyUlt;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite fullStar;
    public Sprite emptyStar;
    public TMP_Text Ult_t;
    public PlayerController playerController;
    public GameManager gameManager;


    private void Start()
    {
        Time.timeScale = 1f;
        UpdateHearts();

    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
                SettingPauseGame();
            else
                SettingResumeGame();
        }
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            SetStageMenuStar();
        }
        EndGameResult();
        UpdateHearts();
        UpdateUlt();
   
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Returntomenu()
    {
        Time.timeScale = 1f;
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
            stageMenu.SetActive(true);
        else
            Debug.LogWarning("StageMenu 오브젝트를 찾을 수 없습니다!");
    }

    public void RetryStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        Debug.Log("해당 스테이지를 재시작합니다.");
    }

    public void PauseGame()
    {
        Debug.Log("게임을 일시중지합니다");
        Time.timeScale = 0f;
    }

    public void SettingPauseGame()
    {
        PauseGame();
        if (settingMenu != null)
            settingMenu.SetActive(true);
        else
            Debug.LogWarning("settingMenu 오브젝트를 찾을 수 없습니다!");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("게임을 재개합니다.");
    }

    public void SettingResumeGame()
    {
        ResumeGame();
        if (settingMenu != null)
            settingMenu.SetActive(false);
        else
            Debug.LogWarning("settingMenu 오브젝트를 찾을 수 없습니다!");
    }

    public void StageMenuClose()
    {
        if (stageMenu != null)
            stageMenu.SetActive(false);
        else
            Debug.LogWarning("StageMenu 오브젝트를 찾을 수 없습니다!");
    }

    private void UpdateHearts()
    {
        if (playerController == null) return;

        float currentHP = playerController.hp;
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = (i < currentHP) ? fullHeart : emptyHeart;
        }
    }

    private void UpdateUlt()
    {
        if (playerController == null) return;

        int currentUlt = playerController.Ult;
        Ult_t.text = currentUlt + "%";
        ultImages[0].sprite = (currentUlt >= 100) ? fullUlt : emptyUlt;
    }

    private void EndGameResult()
    {
        if (gameManager == null || SaveManager.instance == null) return;

        if (!gameManager.isStageClear) return;

        string stageName = SceneManager.GetActiveScene().name;

        // 이미 저장된 별점이 현재보다 크거나 같으면 저장하지 않음
        int existingStar = SaveManager.instance.GetStageStar(stageName);
        int currentStar = gameManager.Starcount;

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = (i < currentStar) ? fullStar : emptyStar;
        }

        if (currentStar <= existingStar) return; // 이미 저장된 것이 더 좋거나 같으면 무시

        SaveManager.instance.SetStageStar(stageName, currentStar);
        
        

        Debug.Log($"[Result] Stage {stageName} Clear - Stars: {currentStar}");
    }

    public void SetStageMenuStar()
    {
        UpdateStageStars("Stage1", 0, 3); // Stage1의 별은 0 ~ 2번
        UpdateStageStars("Stage2", 3, 6); // Stage2의 별은 3 ~ 5번
        UpdateStageStars("Stage3", 6, 9); // Stage3의 별은 6 ~ 8번
        UpdateStageStars("Stage4", 9, 12); // Stage4의 별은 9 ~ 11번
    }
    private void UpdateStageStars(string stageName, int startIndex, int endIndex)
    {
        // null 체크 및 로그 추가
        if (stageName == null)
        {
            Debug.LogError("Stage name is null!");  // stageName이 null인 경우 로그
            return;  // null이라면 함수를 더 이상 실행하지 않음
        }

        if (SaveManager.instance == null)
        {
            Debug.LogError("SaveManager instance is null!");  // SaveManager가 null인 경우 로그
            return;  // instance가 null이라면 더 이상 진행하지 않음
        }

        // getStarCount를 구하는 부분
        int getStarCount = SaveManager.instance.GetStageStar(stageName);

        // 예외가 발생할 수 있는 부분을 체크
        if (getStarCount < 0)
        {
            Debug.LogWarning($"Invalid star count for {stageName}: {getStarCount}. Setting to 0.");
            getStarCount = 0;  // getStarCount가 잘못된 값일 경우 0으로 설정
        }

        Debug.Log($"Stage: {stageName}, Star Count: {getStarCount}");

        // 해당 스테이지에 대한 별 이미지를 업데이트
        for (int i = startIndex; i < endIndex; i++)
        {
            starImages[i].sprite = (i - startIndex < getStarCount) ? fullStar : emptyStar;
        }
    }
}
