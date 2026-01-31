using UnityEngine;

public class SoundTestSc : MonoBehaviour
{
    public AudioSource source1;
    public AudioClip clip1;

    void Start()
    {
        //
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SoundManager.Instance.PlaySE(source1);
    }
}
