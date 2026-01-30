using UnityEngine;

public class NewsTicker : MonoBehaviour
{
    public RectTransform textTransform;
    public float scrollSpeed = 100f;
    public float startX = 800f;
    public float endX = -800f;

    void Update()
    {
        // Move text to the left
        textTransform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Reset position when it goes off screen
        if (textTransform.anchoredPosition.x < endX)
        {
            textTransform.anchoredPosition = new Vector2(startX, textTransform.anchoredPosition.y);
        }
    }
}
