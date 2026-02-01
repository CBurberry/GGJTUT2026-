using UnityEngine;

public class MaskingSphere : MonoBehaviour
{
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 cameradir = Camera.main.transform.position-transform.position;
        transform.position = startPosition + cameradir*2;


    }
}
