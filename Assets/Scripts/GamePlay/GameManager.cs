using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int targetScore = 8, lives = 3;
    [SerializeField] bool scoreWins = true;          // false = coins are bonus only
    [SerializeField] bool goalNeedsScore = false;    // true = exit locked until targetScore coins
    [SerializeField] Renderer goalPad;               // optional: turns red/green with the lock
    [SerializeField] float timeLimit = 0f;           // 0 = no timer
    [SerializeField] bool surviveToWin = false;      // true = timer reaching 0 is a win
    [SerializeField] string timerLabel = "Time";
    [SerializeField] float respawnDelay = 0.6f, spawnProtection = 1.5f;
    [SerializeField] Health player;
    [SerializeField] Transform respawnPoint;
    [SerializeField] Hud hud;                        // the HUD prefab's root

    int score, bonus;
    string lastScoreLine, lastLivesLine, lastTimerLine;
    float timeLeft;
    bool ended;
    bool respawning;

    public bool Ended => ended;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        timeLeft = timeLimit;
    }

    void Start()
    {
        hud.SetLevel("Level " + gameObject.scene.buildIndex);
        hud.ShowTimer(timeLimit > 0f);
        hud.ShowScore(targetScore > 0);
        SetGoalColor();
        UpdateUI();
    }

    void Update()
    {
        if (ended || timeLimit <= 0f) return;
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            timeLeft = 0f; UpdateUI();
            if (surviveToWin) Win(); else Lose();
            return;
        }
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (ended) return;
        score += amount;
        SetGoalColor();
        UpdateUI();
        if (scoreWins && targetScore > 0 && score >= targetScore) Win();
    }

    public void CollectRedCoin(Vector3 playerPosition) { AddScore(1); }
    public void AddBonus() { bonus++; }

    public void ReachGoal()
    {
        if (goalNeedsScore && score < targetScore) return;
        Win();
    }

    void SetGoalColor()
    {
        if (goalPad == null || !goalNeedsScore) return;
        goalPad.material.color = score >= targetScore ? Pal.Pickup : Pal.Hazard;
    }

    public void UpdateUI()
    {
        if (targetScore > 0) SetIfChanged(hud.SetScore, ref lastScoreLine, ScoreLine());
        if (player != null)
            SetIfChanged(hud.SetLives, ref lastLivesLine, $"Lives: {lives}");
        if (timeLimit > 0f)
            SetIfChanged(hud.SetTimer, ref lastTimerLine, $"{timerLabel}: {Mathf.CeilToInt(timeLeft)}");
    }

    string ScoreLine()
    {
        if (goalNeedsScore)
        {
            string state = score >= targetScore ? "EXIT OPEN" : "exit locked";
            return $"Coins: {score} / {targetScore}   {state}";
        }
        if (scoreWins) return $"Score: {score} / {targetScore}";
        return $"Bonus coins: {score} / {targetScore}";
    }

    void SetIfChanged(System.Action<string> set, ref string last, string value)
    {
        if (value == last) return;
        set(value);
        last = value;
    }

    public void PlayerDied()
    {
        if (ended || respawning) return;
        lives--;
        UpdateUI();
        if (lives <= 0) { Lose(); return; }
        respawning = true;
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        Rigidbody body = player.GetComponent<Rigidbody>();
        body.position = respawnPoint.position;
        body.linearVelocity = Vector3.zero;
        player.Restore(spawnProtection);
        respawning = false;
    }

    void Win()
    {
        if (ended) return;
        ended = true;
        int level = gameObject.scene.buildIndex;
        float time = Time.timeSinceLevelLoad;
        bool best = SaveData.Record(level, time);
        SaveData.Unlock(level + 1);
        SfxPlayer.Do(Sfx.Win);
        hud.ShowWin(time, best, SaveData.GetBest(level));
        Time.timeScale = 0f;
    }

    void Lose()
    {
        if (ended) return;
        ended = true;
        SfxPlayer.Do(Sfx.Lose);
        hud.ShowLose();
        Time.timeScale = 0f;
    }
}
