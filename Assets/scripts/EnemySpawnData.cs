using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public float spawnTime;
    public GameObject enemyPrefab;
    public Vector2 spawnPosition;
    public string patternId;
}