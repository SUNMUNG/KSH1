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
        PlaySFX_Click();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Returntomenu()
    {
        Time.timeScale = 1f;
        PlaySFX_Click();
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayStageStart(string name)
    {
        PlaySFX_Click();    
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
        PlaySFX_Click();
        SceneManager.LoadScene("SettingMenu");
    }

    public void StageMenuOpen()
    {
        PlaySFX_Click();
        if (stageMenu != null)
            stageMenu.SetActive(true);
        else
            Debug.LogWarning("StageMenu ������Ʈ�� ã�� �� �����ϴ�!");
    }

    public void RetryStage()
    {
        PlaySFX_Click();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        Debug.Log("�ش� ���������� ������մϴ�.");
    }

    public void PauseGame()
    {
        PlaySFX_Click();
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
        PlaySFX_Click();
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

    public void ResetGame()
    {
        SaveManager.instance.ResetGame();
        PlaySFX_Click();
        SceneManager.LoadScene("MainMenu");
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

        if (!gameManager.isStageClear) return;

        string stageName = SceneManager.GetActiveScene().name;

        // �̹� ����� ������ ���纸�� ũ�ų� ������ �������� ����
        int existingStar = SaveManager.instance.GetStageStar(stageName);
        int currentStar = gameManager.Starcount;

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = (i < currentStar) ? fullStar : emptyStar;
        }

        if (currentStar <= existingStar) return; // �̹� ����� ���� �� ���ų� ������ ����

        SaveManager.instance.SetStageStar(stageName, currentStar);
        
        

        Debug.Log($"[Result] Stage {stageName} Clear - Stars: {currentStar}");
    }

    public void SetStageMenuStar()
    {
        UpdateStageStars("Stage1", 0, 3); // Stage1�� ���� 0 ~ 2��
        UpdateStageStars("Stage2", 3, 6); // Stage2�� ���� 3 ~ 5��
        UpdateStageStars("Stage3", 6, 9); // Stage3�� ���� 6 ~ 8��
        UpdateStageStars("Stage4", 9, 12); // Stage4�� ���� 9 ~ 11��
    }
    public void PlaySFX_Click(){
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxUI_Click);
    }
    private void UpdateStageStars(string stageName, int startIndex, int endIndex)
    {
        // null üũ �� �α� �߰�
        if (stageName == null)
        {
            Debug.LogError("Stage name is null!");  // stageName�� null�� ��� �α�
            return;  // null�̶�� �Լ��� �� �̻� �������� ����
        }

        if (SaveManager.instance == null)
        {
            Debug.LogError("SaveManager instance is null!");  // SaveManager�� null�� ��� �α�
            return;  // instance�� null�̶�� �� �̻� �������� ����
        }

        // getStarCount�� ���ϴ� �κ�
        int getStarCount = SaveManager.instance.GetStageStar(stageName);

        // ���ܰ� �߻��� �� �ִ� �κ��� üũ
        if (getStarCount < 0)
        {
            Debug.LogWarning($"Invalid star count for {stageName}: {getStarCount}. Setting to 0.");
            getStarCount = 0;  // getStarCount�� �߸��� ���� ��� 0���� ����
        }

        Debug.Log($"Stage: {stageName}, Star Count: {getStarCount}");

        // �ش� ���������� ���� �� �̹����� ������Ʈ
        for (int i = startIndex; i < endIndex; i++)
        {
            starImages[i].sprite = (i - startIndex < getStarCount) ? fullStar : emptyStar;
        }
    }
}
