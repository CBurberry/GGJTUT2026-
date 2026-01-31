using UnityEngine;

public class VoiceController : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] private AudioSource source;

    [SerializeField] float time;
    [SerializeField] int clipNum;
    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time > 6)
        {
            clipNum = Random.Range(0,clips.Length);
            source.PlayOneShot(clips[clipNum]);
            time -= 6;
        }
    }
}
