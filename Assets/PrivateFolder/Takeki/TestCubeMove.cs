using Unity.VisualScripting;
using UnityEngine;

public class TestCubeMove : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 5f * Time.deltaTime);
    }
}
