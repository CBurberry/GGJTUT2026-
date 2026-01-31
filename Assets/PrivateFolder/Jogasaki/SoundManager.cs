using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource audioSource;
    //public int poolSize;
    //private List<AudioSource> pool = new List<AudioSource>();

    //public Transform listener;

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

        //for (int i = 0; i < poolSize; i++)
        //{
        //    AudioSource s = Instantiate(sourcePrefab, transform);
        //    s.spatialBlend = 0;
        //    pool.Add(s);
        //}
    }
    void Start()
    {
        
    }

    void Update()
    {
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

    public void PlayMaskSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
