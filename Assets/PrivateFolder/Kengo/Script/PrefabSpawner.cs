using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnInterval = 1f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
