using UnityEngine;
using UnityEngine.Audio;

public enum Sfx { Pickup, Hit, Button, Win, Lose }

public class SfxPlayer : MonoBehaviour
{
    public static SfxPlayer Instance { get; private set; }

    [SerializeField] AudioMixerGroup group;
    [SerializeField] AudioClip pickup, hit, button, win, lose;
    [SerializeField] float semitones = 2f;

    AudioSource[] pool;
    int next;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        pool = new AudioSource[4];
        for (int i = 0; i < pool.Length; i++)
        {
            var go = new GameObject("Sfx" + i);
            go.transform.SetParent(transform);
            var s = go.AddComponent<AudioSource>();
            s.playOnAwake = false;
            s.outputAudioMixerGroup = group;
            s.ignoreListenerPause = true;
            pool[i] = s;
        }
    }

    public void Play(AudioClip clip)
    {
        if (clip == null) return;
        AudioSource s = pool[next];
        next = (next + 1) % pool.Length;
        s.pitch = Mathf.Pow(2f, Random.Range(-semitones, semitones) / 12f);
        s.PlayOneShot(clip);
    }

    // null-safe, so a level opened on its own still runs
    public static void Do(Sfx which)
    {
        if (Instance == null) return;
        AudioClip c = which switch
        {
            Sfx.Pickup => Instance.pickup, Sfx.Hit => Instance.hit, Sfx.Button => Instance.button,
            Sfx.Win => Instance.win, _ => Instance.lose
        };
        Instance.Play(c);
    }
}
