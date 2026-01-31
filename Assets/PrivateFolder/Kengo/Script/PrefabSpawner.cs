using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SpawnPrefabData
{
    public GameObject prefab;
    [Range(0f, 100f)]
    public float probability;
}

[System.Serializable]
public class SpawnWave
{
    [Header("このWaveが続く秒数（-1で無限）")]
    public float duration = 10f;

    [Header("このWaveの出現Prefabと確率")]
    public List<SpawnPrefabData> spawnPrefabs = new List<SpawnPrefabData>();
}

public class PrefabSpawner : MonoBehaviour
{
    [Header("Wave設定")]
    public List<SpawnWave> waves = new List<SpawnWave>();

    [Header("スポーン間隔")]
    public float spawnInterval = 1f;

    private int currentWaveIndex = 0;
    private float waveTimer = 0f;
    private float spawnTimer = 0f;

    void Update()
    {
        if (waves.Count == 0) return;

        spawnTimer += Time.deltaTime;
        waveTimer += Time.deltaTime;

        // スポーン処理
        if (spawnTimer >= spawnInterval)
        {
            Spawn();
            spawnTimer = 0f;
        }

        // Wave切り替え（最後のWaveは無限）
        SpawnWave currentWave = waves[currentWaveIndex];
        if (currentWave.duration >= 0f && waveTimer >= currentWave.duration)
        {
            if (currentWaveIndex < waves.Count - 1)
            {
                currentWaveIndex++;
                waveTimer = 0f;
            }
        }
    }

    void Spawn()
    {
        SpawnWave wave = waves[currentWaveIndex];
        if (wave.spawnPrefabs.Count == 0) return;

        GameObject prefab = GetRandomPrefab(wave.spawnPrefabs);
        if (prefab == null) return;

        Instantiate(prefab, transform.position, Quaternion.identity);
    }

    GameObject GetRandomPrefab(List<SpawnPrefabData> list)
    {
        float totalProbability = 0f;

        foreach (var data in list)
        {
            totalProbability += data.probability;
        }

        float randomValue = Random.Range(0f, totalProbability);
        float current = 0f;

        foreach (var data in list)
        {
            current += data.probability;
            if (randomValue <= current)
            {
                return data.prefab;
            }
        }

        return null;
    }
}
