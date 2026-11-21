using TMPro;
using UnityEngine;

// Root of the HUD prefab. GameManager talks to this one object only.
public class Hud : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText, livesText, timerText, levelText, winTimeText, winBestText;
    [SerializeField] GameObject scorePanel, timerPanel, winPanel, losePanel;
    [SerializeField] LevelLoader loader;

    public void SetScore(string s) { if (scoreText) scoreText.text = s; }
    public void SetLives(string s) { if (livesText) livesText.text = s; }
    public void SetTimer(string s) { if (timerText) timerText.text = s; }
    public void SetLevel(string s) { if (levelText) levelText.text = s; }
    public void ShowScore(bool on) { if (scorePanel) scorePanel.SetActive(on); }
    public void ShowTimer(bool on) { if (timerPanel) timerPanel.SetActive(on); }

    public void ShowWin(float seconds, bool newBest, float best)
    {
        winTimeText.text = $"Time  {seconds:0.0} s";
        winBestText.text = newBest ? "NEW BEST!" : $"Best  {best:0.0} s";
        winPanel.SetActive(true);
    }
    public void ShowLose() => losePanel.SetActive(true);

    // buttons on the win and game-over screens
    public void NextLevel() => loader.LoadNext();
    public void Restart() => loader.Reload();
    public void MainMenu() => loader.LoadMenu();
}
