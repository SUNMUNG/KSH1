using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMesh Pro를 사용하기 위한 네임스페이스 추가
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController; // 플레이어 컨트롤러
    public int score = 0; // 점수
    public Text hp; // HP 텍스트
    public Text score_T; // 점수 텍스트
    public GameObject Stage1_enemyPrefab1;
    public GameObject Stage1_enemyPrefab2;
    public GameObject Stage1_enemyPrefab3; // 적 프리팹
    public GameObject Stage1_enemyPrefab4;
    public GameObject Stage1_enemyPrefab5;
    public GameObject Stage1_enemyPrefab6;
    public GameObject Stage1_enemyPrefab7;
    public float spawnInterval = 3f; // 적 스폰 간격
    public TMP_Text gameOverText; // 게임 오버 텍스트
    public GameObject gameClearUI;
    public int Starcount;
    private float spawnY = 6f;
    private float nextSpawnTime;
    public int waveCount = 0; // 웨이브 카운트 추가
    public string stageName;
    private int enemiesSpawnedInWave = 0; // 현재 웨이브에 스폰된 적의 수
    private int enemiesInWave = 10; // 한 웨이브마다 스폰할 적의 수
    public bool isStageClear = false; // 스테이지 클리어 확인용

    void UpdateScore()
    {
        score_T.text = "Score : " + score.ToString();
    }

    // 적 스폰 메서드
    void SpawnEnemy_Stage1()
    {
        if (isStageClear==true)
        {
            return;
        }
        if (Time.time >= nextSpawnTime)
        {
            GameObject enemyPrefab = GetRandomEnemyPrefab(); // 랜덤으로 적 프리팹 선택

            if (enemyPrefab != null && enemiesSpawnedInWave < enemiesInWave)
            {
                // 랜덤 X 좌표 계산
                float randomX = UnityEngine.Random.Range(-7f, 7f); // X 좌표 랜덤

                // Y 좌표는 항상 일정하게 고정
                Vector2 spawnPosition = new Vector2(randomX, spawnY);

                // 적 생성
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(180, 0, 0));

                // 스폰된 적의 위치를 강제로 고정
                enemy.transform.position = new Vector2(randomX, spawnY); // 위치를 강제로 설정
                Debug.Log("Spawned Enemy at position: " + enemy.transform.position);

                enemiesSpawnedInWave++; // 한 웨이브에서 스폰된 적의 수 증가
            }
            else if (enemiesSpawnedInWave >= enemiesInWave)
            {
                // 한 웨이브에서 적이 모두 스폰되었을 경우
                Debug.Log("Wave " + waveCount + " Completed!");
                enemiesSpawnedInWave = 0; // 웨이브 카운트 리셋
                waveCount++; // 웨이브 증가
                Debug.Log("Wave Count: " + waveCount);
            }
            nextSpawnTime = Time.time + spawnInterval; // 적 스폰 간격 설정
        }
    }
    void SpawnEnemy_Stage2()
    {
        if (isStageClear) return;

        if (Time.time >= nextSpawnTime)
        {
            // 적 프리팹 배열
            GameObject[] enemyPrefabs = new GameObject[]
            {
            Stage1_enemyPrefab3, Stage1_enemyPrefab6, Stage1_enemyPrefab7
            };

            // 7번 프리팹이 이미 존재하는지 체크
            bool isPrefab7Exists = GameObject.Find("BasicEnemy_Circle(Clone)") != null;

            // 7번 프리팹이 존재하면 다른 프리팹 선택
            GameObject selectedPrefab;
            if (isPrefab7Exists)
            {
                // "Stage1_enemyPrefab7"이 이미 존재하면, 나머지 프리팹 중에서 랜덤 선택
                selectedPrefab = enemyPrefabs[UnityEngine.Random.Range(0, 2)];
                Debug.Log("프리팹이 이미 존재합니다.");// Stage1_enemyPrefab3, Stage1_enemyPrefab6 중 하나
            }
            else
            {
                // 7번 프리팹을 포함한 모든 프리팹 중에서 랜덤 선택
                selectedPrefab = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
                Debug.Log("프리팹이 이미 존재하지않습니다.");
            }

            // 랜덤 X 좌표
            float randomX = UnityEngine.Random.Range(-6f, 6f);
            Vector2 spawnPosition = new Vector2(randomX, spawnY);

            // 생성
            GameObject enemy = Instantiate(selectedPrefab, spawnPosition, Quaternion.Euler(180, 0, 0));

            // movementPattern 랜덤 지정
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                int patternIndex = UnityEngine.Random.Range(0, 4); // 0~3
                enemyScript.movementPattern = (Enemy.MovementPattern)patternIndex;

                // 탄막도 shoot 오버라이드해서 사용 가능
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


    // 랜덤으로 적 프리팹을 선택하는 메서드
    GameObject GetRandomEnemyPrefab()
    {
        // 모든 적 프리팹을 배열에 담아 랜덤으로 선택
        GameObject[] enemyPrefabs = new GameObject[] { Stage1_enemyPrefab1, Stage1_enemyPrefab2, Stage1_enemyPrefab3 };

        // 랜덤 인덱스 생성 (0부터 enemyPrefabs.Length-1까지)
        int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);

        // 랜덤으로 선택된 적 프리팹 반환
        return enemyPrefabs[randomIndex];
    }

    // 게임 오버 체크 메서드
    void CheckGameOver()
    {
        if (playerController.HPOut() <= 0)
        {
            gameOverText.gameObject.SetActive(true); // 게임 오버 텍스트 표시
            Time.timeScale = 0; // 게임 멈추기
        }
    }
    // 모든 적이 죽었는지 확인하고, 스테이지 클리어 메시지 출력
    void CheckStageClear()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && waveCount == 3) // 적이 하나도 남지 않았을 때
        {
            isStageClear = true;
            gameClearUI.gameObject.SetActive(true);
            Time.timeScale = 0; // 게임 멈추기
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
            SpawnEnemy_Stage1(); // 적 스폰
        }
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {
            SpawnEnemy_Stage2(); // 적 스폰
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            SpawnEnemy_Stage3(); // 적 스폰
        }
        else if (SceneManager.GetActiveScene().name == "Stage4")
        {
            SpawnEnemy_Stage4(); // 적 스폰
        }
        else { Debug.Log("해당하는 스테이지가 없습니다"); }
    }


    void Update()
    {
        StageSpawnSelect();
        UpdateScore();
        // 게임 오버 체크
        CheckGameOver();
        // 모든 적이 죽었는지 확인 (스테이지 클리어 체크)
        CheckStageClear();
    }
}
