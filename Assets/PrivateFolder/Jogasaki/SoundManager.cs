using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public float AudioVol = 1.0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
    }

    public void PlaySE(AudioSource source, AudioClip clip = null)
    {
        source.volume = AudioVol;
        if (clip != null)
            source.PlayOneShot(clip);
        else source.PlayOneShot(source.clip);
    }

    public void PlayBGM(AudioSource source, AudioClip clip = null)
    {
        if (clip != null)
            source.clip = clip;
        else source.Play();
    }


}
