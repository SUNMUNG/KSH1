
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class EnemySpawnTimelineCreator
{
    [MenuItem("Tools/Create Sample Stage Timeline")]
    public static void CreateSampleTimeline()
    {
        string path = "Assets/Resources/Stages";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        EnemySpawnTimeline timeline = ScriptableObject.CreateInstance<EnemySpawnTimeline>();

        GameObject StraightEnemy = Resources.Load<GameObject>("Prefabs/BasicEnemy_Straight");
        GameObject StarEnemy = Resources.Load<GameObject>("Prefabs/BasicEnemy_Star");
        GameObject SpreadEnemy = Resources.Load<GameObject>("Prefabs/BasicEnemy_Spread");
        GameObject CircleEnemy = Resources.Load<GameObject>("Prefabs/BasicEnemy_Circle");
        GameObject ChaseEnemy = Resources.Load<GameObject>("Prefabs/SpecialEnemy_Chase");
        GameObject SpiralEnemy = Resources.Load<GameObject>("Prefabs/SpecialEnemy_Spiral");
        GameObject WaveEnemy = Resources.Load<GameObject>("Prefabs/SpecialEnemy_Wave");
        

        timeline.spawns = new List<EnemySpawnData> {
            new EnemySpawnData { spawnTime = 0f, enemyPrefab = StraightEnemy, spawnPosition = new Vector2(-3, 5), patternId = "spiral" },
            new EnemySpawnData { spawnTime = 1.5f, enemyPrefab = StarEnemy, spawnPosition = new Vector2(3, 5), patternId = "spiral" },
            new EnemySpawnData { spawnTime = 3f, enemyPrefab = SpreadEnemy, spawnPosition = new Vector2(-4, 4), patternId = "cross_shot" },
            new EnemySpawnData { spawnTime = 3f, enemyPrefab = SpreadEnemy, spawnPosition = new Vector2(4, 4), patternId = "cross_shot" },
            new EnemySpawnData { spawnTime = 5f, enemyPrefab = CircleEnemy, spawnPosition = new Vector2(0, 5), patternId = "rotating_circle" },
            new EnemySpawnData { spawnTime = 8f, enemyPrefab = StraightEnemy, spawnPosition = new Vector2(0, 5), patternId = "midboss_phase1" },
            new EnemySpawnData { spawnTime = 15f, enemyPrefab = ChaseEnemy, spawnPosition = new Vector2(3, 5), patternId = "rush" },
            new EnemySpawnData { spawnTime = 16f, enemyPrefab = ChaseEnemy, spawnPosition = new Vector2(-3, 5), patternId = "rush" },
            new EnemySpawnData { spawnTime = 17f, enemyPrefab = SpiralEnemy, spawnPosition = new Vector2(0, 5), patternId = "reverse_spread" },
            new EnemySpawnData { spawnTime = 20f, enemyPrefab = WaveEnemy, spawnPosition = new Vector2(0, 5), patternId = "boss_intro" }
        };

        string assetPath = $"{path}/Stage1Timeline.asset";
        AssetDatabase.CreateAsset(timeline, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Stage1Timeline 생성 완료: " + assetPath);
    }
}