// HitSfx.cs
using UnityEngine;

public class HitSfx : MonoBehaviour
{
    Health health;
    void OnEnable()
    {
        health = GetComponentInParent<Health>();
        if (health != null) health.Damaged += OnHit;
    }
    void OnDisable() { if (health != null) health.Damaged -= OnHit; }
    void OnHit(int current, int max) => SfxPlayer.Do(Sfx.Hit);
}