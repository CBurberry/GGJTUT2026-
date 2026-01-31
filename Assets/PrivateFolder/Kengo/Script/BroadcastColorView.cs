using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BroadcastColorView : MonoBehaviour
{
    private Renderer rend;
    private ObjClickJudge judge;

    [Header("ï\é¶êF")]
    public Color goodColor = Color.red;
    public Color badColor = Color.blue;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        judge = GetComponent<ObjClickJudge>();

        if (judge == null)
        {
            Debug.LogError($"{name}: ObjClickJudge Ç™å©Ç¬Ç©ÇËÇ‹ÇπÇÒ");
            enabled = false;
            return;
        }

        // èâä˙èÛë‘îΩâf
        UpdateColor();
    }

    void OnEnable()
    {
        if (judge != null)
        {
            judge.OnClicked += UpdateColor;
        }
    }

    void OnDisable()
    {
        if (judge != null)
        {
            judge.OnClicked -= UpdateColor;
        }
    }

    private void UpdateColor()
    {
        rend.material.color = judge.IsGoodBroadcasting ? goodColor : badColor;
    }
}
