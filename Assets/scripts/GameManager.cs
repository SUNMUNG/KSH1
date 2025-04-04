using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMesh Pro�� ����ϱ� ���� ���ӽ����̽� �߰�

public class GameManager : MonoBehaviour
{
    public PlayerController playerController; // �÷��̾� ��Ʈ�ѷ�
    public int score = 0; // ����
    public Text hp; // HP �ؽ�Ʈ
    public Text score_T; // ���� �ؽ�Ʈ
    public GameObject Stage1_enemyPrefab1;
    public GameObject Stage1_enemyPrefab2;
    public GameObject Stage1_enemyPrefab3; // �� ������
    public float spawnInterval = 3f; // �� ���� ����
    public TMP_Text gameOverText; // ���� ���� �ؽ�Ʈ (TextMesh Pro ���)
    private float spawnY = 6f;
    private float nextSpawnTime;
    private int waveCount = 0; // ���̺� ī��Ʈ �߰� (���̺긶�� ���̵� ��ȭ)

    void UpdateHPText()
    {
        hp.text = "HP: " + playerController.HPOut().ToString();
    }

    void UpdateScore()
    {
        score_T.text = "Score : " + score.ToString();
    }

    // �� ���� �޼���
    void SpawnEnemy_Stage1()
    {
        if (Time.time >= nextSpawnTime)
        {
            GameObject enemyPrefab = GetRandomEnemyPrefab(); // �������� �� ������ ����

            if (enemyPrefab != null)
            {
                // ���� X ��ǥ ���
                float randomX = Random.Range(-7f, 7f); // X ��ǥ ����

                // Y ��ǥ�� �׻� �����ϰ� ����
                Vector2 spawnPosition = new Vector2(randomX, spawnY);

                // �� ����
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(180, 0, 0));

                // ������ ���� ��ġ�� ������ ����
                enemy.transform.position = new Vector2(randomX, spawnY); // ��ġ�� ������ ����
                Debug.Log("Spawned Enemy at position: " + enemy.transform.position);
            }
            else
            {
                Debug.LogWarning("No enemy prefab assigned!");
            }
            nextSpawnTime = Time.time + spawnInterval; // �� ���� ���� ����
        }
    }

    // �������� �� �������� �����ϴ� �޼���
    GameObject GetRandomEnemyPrefab()
    {
        // ��� �� �������� �迭�� ��� �������� ����
        GameObject[] enemyPrefabs = new GameObject[] { Stage1_enemyPrefab1, Stage1_enemyPrefab2, Stage1_enemyPrefab3 };

        // ���� �ε��� ���� (0���� enemyPrefabs.Length-1����)
        int randomIndex = Random.Range(0, enemyPrefabs.Length);

        // �������� ���õ� �� ������ ��ȯ
        return enemyPrefabs[randomIndex];
    }

    // ���� ���� üũ �޼���
    void CheckGameOver()
    {
        if (playerController.HPOut() <= 0)
        {
            gameOverText.gameObject.SetActive(true); // ���� ���� �ؽ�Ʈ ǥ��
            Time.timeScale = 0; // ���� ���߱�
        }
    }

    void Update()
    {
        UpdateHPText();
        UpdateScore();
        SpawnEnemy_Stage1(); // �� ����

        // ���� ���� üũ
        CheckGameOver();

        // ���̺� ����
        waveCount++; // ��� ����Ǹ� ���̺� ����
    }
}
