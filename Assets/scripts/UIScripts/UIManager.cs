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
        Debug.Log(name + "�� �ҷ��ɴϴ�.");
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
            Debug.LogWarning("StageMenu ������Ʈ�� ã�� �� �����ϴ�!");
    }

    public void RetryStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        Debug.Log("�ش� ���������� ������մϴ�.");
    }

    public void PauseGame()
    {
        Debug.Log("������ �Ͻ������մϴ�");
        Time.timeScale = 0f;
    }

    public void SettingPauseGame()
    {
        PauseGame();
        if (settingMenu != null)
            settingMenu.SetActive(true);
        else
            Debug.LogWarning("settingMenu ������Ʈ�� ã�� �� �����ϴ�!");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("������ �簳�մϴ�.");
    }

    public void SettingResumeGame()
    {
        ResumeGame();
        if (settingMenu != null)
            settingMenu.SetActive(false);
        else
            Debug.LogWarning("settingMenu ������Ʈ�� ã�� �� �����ϴ�!");
    }

    public void StageMenuClose()
    {
        if (stageMenu != null)
            stageMenu.SetActive(false);
        else
            Debug.LogWarning("StageMenu ������Ʈ�� ã�� �� �����ϴ�!");
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
