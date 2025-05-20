using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Stage/Enemy Spawn Timeline")]
public class EnemySpawnTimeline : ScriptableObject
{
    public List<EnemySpawnData> spawns = new List<EnemySpawnData>();
}