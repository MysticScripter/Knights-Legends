using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        foreach (Sound sound in this.sounds)
        {
            sound.source = this.gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
            this.Play("Menu");
        else
            this.Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = null;
        foreach (Sound target in sounds)
        {
            if (target.name == name)
            {
                s = target;
                break;
            }
        }
        s.source.Play();
    }
}
