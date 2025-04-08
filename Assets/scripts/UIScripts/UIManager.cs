using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject stageMenu;
    public Image[] heartImages;  // ��Ʈ �̹������� ���� �迭
    public Sprite fullHeart;     // �� �� ��Ʈ �̹���
    public Sprite emptyHeart;    // �� ��Ʈ �̹���
    public PlayerController playerController;  // PlayerController�� ����
    public void ExitGame()
    {
        // ���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ��� ���� ���� �� ������ ����
#else
            Application.Quit();  // ����� ���ӿ��� ���� ����
#endif
    }
    public void Returntomenu()
    {
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
        // HP ���� ����� ������ ��Ʈ�� ������Ʈ
        UpdateHearts();
    }

    // ��Ʈ �̹����� ���� HP�� �°� ������Ʈ�ϴ� �޼ҵ�
    private void UpdateHearts()
    {
        if (playerController == null) return;  // PlayerController�� ������ ������Ʈ���� ����

        float currentHP = playerController.hp;  // PlayerController���� HP �� ��������

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
}
