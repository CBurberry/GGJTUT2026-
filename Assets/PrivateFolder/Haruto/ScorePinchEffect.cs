using UnityEngine;
using UnityEngine.Rendering.PostProcessing; //

public class ScorePinchEffect : MonoBehaviour
{
    [Header("--- 参照設定 ---")]
    public PostProcessVolume volume; //

    // スコアを持っているスクリプトをここにドラッグします
    // ※もしクラス名が違う場合は、ここを書き換えてください
    public GameManager gameManager;

    private Vignette vignette; //

    void Start()
    {
        // ボリュームからVignetteの設定を読み込む
        if (volume != null)
        {
            volume.profile.TryGetSettings(out vignette);
        }
    }

    void Update()
    {
        if (vignette == null || gameManager == null) return;

        // 【条件】スコアが -8000 以下になったら発動
        // ※gameManagerの中にある実際の「スコア変数名」に合わせてください
        if (gameManager.currentRating <= -8000)
        {
            vignette.enabled.Override(true);
            vignette.color.Override(Color.red); //
            vignette.intensity.Override(0.5f);  //
        }
        else
        {
            // スコアが回復したら赤みを消す
            vignette.intensity.Override(0f);
        }
    }
}