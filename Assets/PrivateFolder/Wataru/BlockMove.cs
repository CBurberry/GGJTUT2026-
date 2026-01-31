using UnityEngine;

public class BlockMove:MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * 5f * Time.deltaTime);
    }
}
