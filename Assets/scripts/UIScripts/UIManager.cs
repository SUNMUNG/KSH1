using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject stageMenu;
    public GameObject settingMenu;
    public Image[] heartImages; // ��Ʈ �̹������� ���� �迭
    public Image[] starImages;
    public Sprite fullHeart;     // �� �� ��Ʈ �̹���
    public Sprite emptyHeart;  // �� ��Ʈ �̹���
    public Sprite fullStar;
    public Sprite emptyStar;
    public PlayerController playerController; // PlayerController�� ����
    public GameManager gameManager;

   
    public void ExitGame()
    {
        // ���� ����

        EditorApplication.isPlaying = false;  // �����Ϳ��� ���� ���� �� ������ ����

            Application.Quit();  // ����� ���ӿ��� ���� ����

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
        {
            stageMenu.SetActive(true);
        }
        else
        {
            Debug.LogWarning("StageMenu ������Ʈ�� ã�� �� �����ϴ�!");
        }
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
       // EditorApplication.isPaused = true;
    }
    public void SettingPauseGame()
    {
        PauseGame();
        if (settingMenu != null)
        {
            settingMenu.SetActive(true);
        }
        else Debug.LogWarning("settingMenu ������Ʈ�� ã�� �� �����ϴ�!");
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
        {
            settingMenu.SetActive(false);
        }
        else Debug.LogWarning("settingMenu ������Ʈ�� ã�� �� �����ϴ�!");
    }
    public void StageMenuClose()
    {

        if (stageMenu != null)
        {
            stageMenu.SetActive(false);
        }
        else
        {
            Debug.LogWarning("StageMenu ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }
    private void Start()
    {
        // �ʱ� HP ���� ������� ��Ʈ ������Ʈ
        UpdateHearts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                SettingPauseGame();
             } else SettingResumeGame();
        }
        // HP ���� ����� ������ ��Ʈ�� ������Ʈ
        UpdateHearts();
        EndGameResult();
    }

    // ��Ʈ �̹����� ���� HP�� �°� ������Ʈ�ϴ� �޼ҵ�
    private void UpdateHearts()
    {
        if (playerController == null) return;  

        float currentHP = playerController.hp ;  // PlayerController���� HP �� ��������

        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHP)
            {
                heartImages[i].sprite = fullHeart;  // �� �� ��Ʈ
            }
            else
            {
                heartImages[i].sprite = emptyHeart;  // �� ��Ʈ
            }
        }
    }

    private void EndGameResult()
    {
        if (gameManager == null)return;  

        int Starcount = gameManager.Starcount;  

        for(int i = 0; i < starImages.Length; i++)
        {
            if (i < Starcount)
            {
                starImages[i].sprite = fullStar;
            }
        }

        
    }
    
}
