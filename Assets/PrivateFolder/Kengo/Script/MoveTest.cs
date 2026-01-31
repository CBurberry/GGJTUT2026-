using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public float minSpeed = 2f;
    public float maxSpeed = 5f;

    private Vector3 moveDirection;
    private float speed;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);

        Camera cam = Camera.main;

        float screenHeight = cam.orthographicSize * 2f;
        float screenWidth = screenHeight * cam.aspect;

        int side = Random.Range(0, 2);

        Vector3 startPos = Vector3.zero;

        switch (side)
        {
            case 0: // ç∂
                startPos = new Vector3(-screenWidth / 2f - 5f, Random.Range(-screenHeight / 2f, screenHeight / 2f), 0);
                moveDirection = Vector3.right;
                break;

            case 1: // âE
                startPos = new Vector3(screenWidth / 2f + 5f, Random.Range(-screenHeight / 2f, screenHeight / 2f), 0);
                moveDirection = Vector3.left;
                break;
        }

        transform.position = startPos;
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;

        if (IsOutOfScreen())
        {
            Destroy(gameObject);
        }
    }

    bool IsOutOfScreen()
    {
        Camera cam = Camera.main;

        float screenHeight = cam.orthographicSize * 2f;
        float screenWidth = screenHeight * cam.aspect;

        Vector3 pos = transform.position;

        return pos.x < -screenWidth / 2f - 10f ||
               pos.x > screenWidth / 2f + 10f ||
               pos.y < -screenHeight / 2f - 10f ||
               pos.y > screenHeight / 2f + 10f;
    }
}
