using UnityEngine;
using UnityEngine.UI;
public class SliderArea:MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] float changeAmount = 10f;

    // ブロックがフレームに入った瞬間
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            slider.value += changeAmount;
        }
    }

    // フレームから出た瞬間に減らしたい場合
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            slider.value -= changeAmount;
        }
    }
}

