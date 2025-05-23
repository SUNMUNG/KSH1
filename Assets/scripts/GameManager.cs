using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMesh Pro�� ����ϱ� ���� ���ӽ����̽� �߰�
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController; // �÷��̾� ��Ʈ�ѷ�
    public int score = 0; // ����
    public Text hp; // HP �ؽ�Ʈ
    public Text score_T; // ���� �ؽ�Ʈ
    public GameObject Stage1_enemyPrefab1;
    public GameObject Stage1_enemyPrefab2;
    public GameObject Stage1_enemyPrefab3; // �� ������
    public GameObject Stage1_enemyPrefab4;
    public GameObject Stage1_enemyPrefab5;
    public GameObject Stage1_enemyPrefab6;
    public GameObject Stage1_enemyPrefab7;
    public float spawnInterval = 3f; // �� ���� ����
    public TMP_Text gameOverText; // ���� ���� �ؽ�Ʈ
    public GameObject gameClearUI;
    public int Starcount;
    private float spawnY = 6f;
    private float nextSpawnTime;
    public int waveCount = 0; // ���̺� ī��Ʈ �߰�
    public string stageName;
    private int enemiesSpawnedInWave = 0; // ���� ���̺꿡 ������ ���� ��
    private int enemiesInWave = 10; // �� ���̺긶�� ������ ���� ��
    public bool isStageClear = false; // �������� Ŭ���� Ȯ�ο�

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
                float randomX = UnityEngine.Random.Range(-7f, 7f); // X ��ǥ ����

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
    void SpawnEnemy_Stage2()
    {
        if (isStageClear) return;

        if (Time.time >= nextSpawnTime)
        {
            // �� ������ �迭
            GameObject[] enemyPrefabs = new GameObject[]
            {
            Stage1_enemyPrefab3, Stage1_enemyPrefab6, Stage1_enemyPrefab7
            };

            // 7�� �������� �̹� �����ϴ��� üũ
            bool isPrefab7Exists = GameObject.Find("BasicEnemy_Circle(Clone)") != null;

            // 7�� �������� �����ϸ� �ٸ� ������ ����
            GameObject selectedPrefab;
            if (isPrefab7Exists)
            {
                // "Stage1_enemyPrefab7"�� �̹� �����ϸ�, ������ ������ �߿��� ���� ����
                selectedPrefab = enemyPrefabs[UnityEngine.Random.Range(0, 2)];
                Debug.Log("�������� �̹� �����մϴ�.");// Stage1_enemyPrefab3, Stage1_enemyPrefab6 �� �ϳ�
            }
            else
            {
                // 7�� �������� ������ ��� ������ �߿��� ���� ����
                selectedPrefab = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
                Debug.Log("�������� �̹� ���������ʽ��ϴ�.");
            }

            // ���� X ��ǥ
            float randomX = UnityEngine.Random.Range(-6f, 6f);
            Vector2 spawnPosition = new Vector2(randomX, spawnY);

            // ����
            GameObject enemy = Instantiate(selectedPrefab, spawnPosition, Quaternion.Euler(180, 0, 0));

            // movementPattern ���� ����
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                int patternIndex = UnityEngine.Random.Range(0, 4); // 0~3
                enemyScript.movementPattern = (Enemy.MovementPattern)patternIndex;

                // ź���� shoot �������̵��ؼ� ��� ����
                enemyScript.InvokeRepeating("shoot", 1f, enemyScript.fireRate);
            }

            enemiesSpawnedInWave++;

            if (enemiesSpawnedInWave >= enemiesInWave)
            {
                Debug.Log("Wave " + waveCount + " Completed!");
                enemiesSpawnedInWave = 0;
                waveCount++;
            }

            nextSpawnTime = Time.time + spawnInterval;
        }
    }
    void SpawnEnemy_Stage3()
    {


    }
    void SpawnEnemy_Stage4()
    {


    }


    // �������� �� �������� �����ϴ� �޼���
    GameObject GetRandomEnemyPrefab()
    {
        // ��� �� �������� �迭�� ��� �������� ����
        GameObject[] enemyPrefabs = new GameObject[] { Stage1_enemyPrefab1, Stage1_enemyPrefab2, Stage1_enemyPrefab3 };

        // ���� �ε��� ���� (0���� enemyPrefabs.Length-1����)
        int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);

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
    void StageSpawnSelect()
    {
        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            SpawnEnemy_Stage1(); // �� ����
        }
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {
            SpawnEnemy_Stage2(); // �� ����
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            SpawnEnemy_Stage3(); // �� ����
        }
        else if (SceneManager.GetActiveScene().name == "Stage4")
        {
            SpawnEnemy_Stage4(); // �� ����
        }
        else { Debug.Log("�ش��ϴ� ���������� �����ϴ�"); }
    }


    void Update()
    {
        StageSpawnSelect();
        UpdateScore();
        // ���� ���� üũ
        CheckGameOver();
        // ��� ���� �׾����� Ȯ�� (�������� Ŭ���� üũ)
        CheckStageClear();
    }
}
