using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip deathSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            // PlayOneShot allows overlapping sounds without interrupting currently playing audio
            audioSource.PlayOneShot(clip);
        }
    }
}