using UnityEngine;

// Listener 1: the health bar.
public class HealthBarBinder : MonoBehaviour
{
    [SerializeField] HealthBar bar;
    Health health;

    void OnEnable()
    {
        GameObject p = GameObject.FindWithTag("Player");
        health = p != null ? p.GetComponent<Health>() : null;
        if (health == null) return;
        health.Damaged += OnChanged;
        health.Healed += OnChanged;
    }

    void OnDisable()
    {
        if (health == null) return;
        health.Damaged -= OnChanged;
        health.Healed -= OnChanged;
    }

    void Start() { if (health != null) { OnChanged(health.Current, health.Max); bar.Set(health.Current, health.Max); } }

    void OnChanged(int current, int max) => bar.Set(current, max);
}
