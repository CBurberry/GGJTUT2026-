using UnityEngine;
using UnityEngine.Audio;

public class SoundNoise : MonoBehaviour
{
    public int outObjNum;

    [SerializeField] private float NoiseVol = -80.0f;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private BroadcastAreaManager BroadcastAreaManager;

    public AudioSource audioSource;
    private bool isNoise;

    void Start()
    {
        audioSource.Play();
        //if(BroadcastAreaManager == null)
        //{
        //    GameObject gameObject = GameObject.Find("");
        //    BroadcastAreaManager = gameObject.GetComponent<BroadcastAreaManager>();
        //}
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.K)) isNoise = true;
        //else isNoise = false;
        //NoiseVolume(BroadcastAreaManager.minusCount);
        audioMixer.SetFloat("NoiseVolume", NoiseVol);
    }

    public void NoiseVolume(int num)
    {
        if (NoiseVol < 0.0f) NoiseVol = -80f + 25.0f * num;
        //SoundManager.Instance.AudioVol = 1.0f - NoiseVol;
    }

}
