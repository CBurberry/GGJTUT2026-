using UnityEngine;

public class SpecialMobSpawn : MonoBehaviour
{
    private int spawnTime;
    [SerializeField] private int firstChangeTime;
    [SerializeField] private int secondChangeTime;
    [SerializeField] private GameManager gameManager;
    void Update()
    {
        spawnTime = gameManager.seconds;

        if (spawnTime > secondChangeTime)
        {
            Debug.Log("SecondChange");
        }
        else if (spawnTime > firstChangeTime) 
        {
            Debug.Log("FirstChange");
        }
    }
}
