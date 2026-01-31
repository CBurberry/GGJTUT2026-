using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private SoundManager Instance;
    //public AudioSource source1;
    //public AudioClip clip1;

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
        //if (Input.GetKeyDown(KeyCode.Space)) PlaySE(source1);
    }

    public void PlaySE(AudioSource source, AudioClip clip = null)
    {
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
