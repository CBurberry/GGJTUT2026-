using UnityEngine;
using UnityEngine.Audio;

public class SoundNoise : MonoBehaviour
{
    public int outObjNum;

    [SerializeField] private float NoiseVol;
    [SerializeField,Header("èâä˙íl")] private float initialValue;
    [SerializeField, Header("ëùâ¡ó¶")] private float minusNum;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private BroadcastAreaManager BroadcastAreaManager;

    public AudioSource audioSource;

    void Start()
    {
        NoiseVol = initialValue;
        audioSource.Play();
        if (BroadcastAreaManager == null)
        {
            GameObject gameObject = GameObject.Find("Canvas");
            BroadcastAreaManager = gameObject.GetComponent<BroadcastAreaManager>();
        }
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.K)) isNoise = true;
        //else isNoise = false;
        NoiseVolume(BroadcastAreaManager.minusCount);
        audioMixer.SetFloat("NoiseVolume", NoiseVol);
    }

    public void NoiseVolume(int num)
    {
        if (initialValue + minusNum * num < 0.0f) NoiseVol = initialValue + minusNum * num;
        else if (initialValue + minusNum * num >= 0.0f) NoiseVol = 0f;
    }

}
