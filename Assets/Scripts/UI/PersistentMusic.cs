using UnityEngine;
using UnityEngine.Audio;

public class PersistentMusic : MonoBehaviour
{
    static PersistentMusic instance;

    [SerializeField] AudioSource music;
    [SerializeField] AudioMixer mixer;

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
        music.loop = true;
        music.Play();
    }

    void Start()
    {
        if (instance != this || mixer == null) return;
        mixer.SetFloat("MusicVolume", Mathf.Log10(SaveData.GetMusicVolume()) * 20f);
    }
}
