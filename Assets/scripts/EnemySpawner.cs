using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnTimeline timeline; // �ν����Ϳ� �巡���ص� �ǰ�, Resources���� �ҷ��͵� ��
    private float elapsedTime = 0f;
    private int nextSpawnIndex = 0;

    void Start()
    {
        // Resources �������� ���� �ҷ��� ���� ����
        if (timeline == null)
        {
            timeline = Resources.Load<EnemySpawnTimeline>("Stages/Stage1Timeline");
        }
    }

    void Update()
    {
        if (timeline == null || nextSpawnIndex >= timeline.spawns.Count) return;

        elapsedTime += Time.deltaTime;

        // ���� ���� �ð��� �Ǹ� �� ����
        while (nextSpawnIndex < timeline.spawns.Count && elapsedTime >= timeline.spawns[nextSpawnIndex].spawnTime)
        {
            var data = timeline.spawns[nextSpawnIndex];
            SpawnEnemy(data);
            nextSpawnIndex++;
        }
    }

    void SpawnEnemy(EnemySpawnData data)
    {
        if (data.enemyPrefab == null)
        {
            Debug.LogWarning("Enemy prefab is missing in spawn data!");
            return;
        }

        Instantiate(data.enemyPrefab, data.spawnPosition, Quaternion.identity);
        // TODO: patternId�� ���� ���� ���� ������ ���� ����
    }
}
