using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SpawnPrefabData
{
    public GameObject prefab;

    [Range(0f, 100f)]
    public float probability;

    [Header("このPrefabのスポーン間隔（空欄＝Waveのデフォルト）")]
    public float customInterval = -1f; // -1ならWave/Spawnerのデフォルト
}

[System.Serializable]
public class SpawnWave
{
    [Header("このWaveが続く秒数（-1で無限）")]
    public float duration = 10f;

    [Header("このWaveの出現Prefabと確率")]
    public List<SpawnPrefabData> spawnPrefabs = new List<SpawnPrefabData>();

    [Header("Waveのデフォルトスポーン間隔")]
    public float defaultInterval = -1f; // -1ならSpawnerの spawnInterval を使う
}

public class PrefabSpawner : MonoBehaviour
{
    [Header("Wave設定")]
    public List<SpawnWave> waves = new List<SpawnWave>();

    [Header("スポーン間隔（Wave/Prefabで指定がなければ使用）")]
    public float spawnInterval = 1f;

    private int currentWaveIndex = 0;
    private float waveTimer = 0f;
    private float spawnTimer = 0f;

    void Update()
    {
        if (waves.Count == 0) return;

        spawnTimer += Time.deltaTime;
        waveTimer += Time.deltaTime;

        // 現在のWave
        SpawnWave currentWave = waves[currentWaveIndex];

        // スポーン処理
        float interval = currentWave.defaultInterval > 0f ? currentWave.defaultInterval : spawnInterval;
        if (spawnTimer >= interval)
        {
            Spawn();
            spawnTimer = 0f;
        }

        // Wave切り替え（最後のWaveは無限）
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

        SpawnPrefabData data = GetRandomPrefabData(wave.spawnPrefabs);
        if (data == null || data.prefab == null) return;

        Instantiate(data.prefab, transform.position, Quaternion.identity);

        // 次回スポーンまでのタイマー調整
        float interval = data.customInterval > 0f ? data.customInterval :
                         (wave.defaultInterval > 0f ? wave.defaultInterval : spawnInterval);
        spawnTimer = -interval + Time.deltaTime; // Update で加算される分を調整
    }

    SpawnPrefabData GetRandomPrefabData(List<SpawnPrefabData> list)
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
                return data;
            }
        }

        return null;
    }
}
