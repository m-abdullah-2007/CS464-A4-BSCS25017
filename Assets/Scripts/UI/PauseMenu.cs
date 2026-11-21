using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    [SerializeField] GameObject panel;
    [SerializeField] LevelLoader loader;

    void Awake() { IsPaused = false; if (panel) panel.SetActive(false); }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (IsPaused) Resume();
        else if (GameManager.Instance == null || !GameManager.Instance.Ended) Pause();
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;               // world sounds stop; SFX + music ignore it
        panel.SetActive(true);                    // SelectOnEnable selects Resume
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        panel.SetActive(false);
    }

    public void Restart() { Resume(); loader.Reload(); }
    public void QuitToMenu() { Resume(); loader.LoadMenu(); }
}
