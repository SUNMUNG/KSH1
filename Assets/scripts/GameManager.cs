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
    public TMP_Text gameOverText; // ���� ���� �ؽ�Ʈ
    public GameObject gameClearUI;
    public int Starcount;
    private float spawnY = 6f;
    private float nextSpawnTime;
    private int waveCount = 0; // ���̺� ī��Ʈ �߰�
    private int enemiesSpawnedInWave = 0; // ���� ���̺꿡 ������ ���� ��
    private int enemiesInWave = 10; // �� ���̺긶�� ������ ���� ��
    private bool isStageClear = false; // �������� Ŭ���� Ȯ�ο�

    void UpdateScore()
    {
        score_T.text = "Score : " + score.ToString();
    }

    // �� ���� �޼���
    void SpawnEnemy_Stage1()
    {
        if (isStageClear==true)
        {
            return;
        }
        if (Time.time >= nextSpawnTime)
        {
            GameObject enemyPrefab = GetRandomEnemyPrefab(); // �������� �� ������ ����

            if (enemyPrefab != null && enemiesSpawnedInWave < enemiesInWave)
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

                enemiesSpawnedInWave++; // �� ���̺꿡�� ������ ���� �� ����
            }
            else if (enemiesSpawnedInWave >= enemiesInWave)
            {
                // �� ���̺꿡�� ���� ��� �����Ǿ��� ���
                Debug.Log("Wave " + waveCount + " Completed!");
                enemiesSpawnedInWave = 0; // ���̺� ī��Ʈ ����
                waveCount++; // ���̺� ����
                Debug.Log("Wave Count: " + waveCount);
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
    // ��� ���� �׾����� Ȯ���ϰ�, �������� Ŭ���� �޽��� ���
    void CheckStageClear()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && waveCount == 3) // ���� �ϳ��� ���� �ʾ��� ��
        {
            isStageClear = true;
            gameClearUI.gameObject.SetActive(true);
            Time.timeScale = 0; // ���� ���߱�
            CheckScoreResult();
        }
    }
    void CheckScoreResult()
    {
        if (score >= 20000 && playerController.Hitnumber <= 5)
        {
            Starcount = 3;
        }
        else if (score >= 15000 && playerController.Hitnumber <= 10)
        {
            Starcount = 2;
        }
        else if (score >= 10000 && playerController.Hitnumber <= 15)
        {
            Starcount = 1;
        }
        else
        {
            Starcount = 0;
        }
    }

    void Update()
    {
        UpdateScore();
        SpawnEnemy_Stage1(); // �� ����

        // ���� ���� üũ
        CheckGameOver();
        // ��� ���� �׾����� Ȯ�� (�������� Ŭ���� üũ)
        CheckStageClear();
    }
}
