using UnityEngine;

public class SoundNoise : MonoBehaviour
{
    public int outObjNum;

    [SerializeField] private float NoiseVol = 0.0f;


    public AudioSource audioSource;
    private bool isNoise;

    void Start()
    {
        audioSource.Play();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.K)) isNoise = true;
        else isNoise = false;
        NoiseVolume(isNoise);
        audioSource.volume = NoiseVol;
    }

    public void NoiseVolume(bool isin)
    {
        if (isin) NoiseVol = 0.5f;
        else NoiseVol = 0.0f;
        SoundManager.Instance.AudioVol = 1.0f - NoiseVol;
    }

}
