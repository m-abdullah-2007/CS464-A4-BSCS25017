using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image fill, lag;
    [SerializeField] float lagDelay = 0.4f, lagSpeed = 0.8f;

    float target;
    float holdUntil;

    public void Set(int current, int max)
    {
        float t = max > 0 ? Mathf.Clamp01((float)current / max) : 0f;
        if (t < target) holdUntil = Time.unscaledTime + lagDelay;   // took damage: lag waits 0.4 s
        else lag.fillAmount = Mathf.Max(lag.fillAmount, t);          // healed: lag jumps up
        target = t;
        fill.fillAmount = t;
    }

    void Update()
    {
        if (Time.unscaledTime < holdUntil || lag.fillAmount <= target) return;
        lag.fillAmount = Mathf.MoveTowards(lag.fillAmount, target, lagSpeed * Time.unscaledDeltaTime);
    }
}
