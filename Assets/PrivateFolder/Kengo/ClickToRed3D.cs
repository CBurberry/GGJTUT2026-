using UnityEngine;

public class ClickToRed3D : MonoBehaviour
{
    private Renderer rend;

    // 外部から参照できるフラグ
    public bool IsRed { get; private set; } = false;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void OnMouseDown()
    {
        //クリックされた！！！！
        SetRed(true);
    }

    public void SetRed(bool value)
    {
        //Debug.Log(" 術式反転　赫");
        IsRed = value;
        rend.material.color = value ? Color.red : Color.white;
    }
}
