using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

        int starCount = gameManager.Starcount;
        string stageName = SceneManager.GetActiveScene().name;

        SaveManager.instance.SetStageStar(stageName, starCount);

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = (i < starCount) ? fullStar : emptyStar;
        }

        Debug.Log($"[Result] Stage {stageName} Clear - Stars: {starCount}");
    }
}
