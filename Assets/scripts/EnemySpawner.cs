using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnTimeline timeline; // 인스펙터에 드래그해도 되고, Resources에서 불러와도 됨
    private float elapsedTime = 0f;
    private int nextSpawnIndex = 0;

    void Start()
    {
        // Resources 폴더에서 직접 불러올 수도 있음
        if (timeline == null)
        {
            timeline = Resources.Load<EnemySpawnTimeline>("Stages/Stage1Timeline");
        }
    }

    void Update()
    {
        if (timeline == null || nextSpawnIndex >= timeline.spawns.Count) return;

        elapsedTime += Time.deltaTime;

        // 다음 스폰 시간이 되면 적 생성
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
        // TODO: patternId에 따라 공격 패턴 설정할 수도 있음
    }
}
