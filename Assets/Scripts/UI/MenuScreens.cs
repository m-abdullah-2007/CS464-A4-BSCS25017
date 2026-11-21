using UnityEngine;

// Main menu + credits glue. Buttons call these.
public class MenuScreens : MonoBehaviour
{
    [SerializeField] LevelLoader loader;
    [SerializeField] GameObject mainPanel, levelPanel;

    public void Play() => loader.Load(Mathf.Clamp(SaveData.GetUnlocked(), 1, 5));
    public void ShowLevels() { mainPanel.SetActive(false); levelPanel.SetActive(true); }
    public void ShowMain() { levelPanel.SetActive(false); mainPanel.SetActive(true); }
    public void Menu() => loader.LoadMenu();
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
