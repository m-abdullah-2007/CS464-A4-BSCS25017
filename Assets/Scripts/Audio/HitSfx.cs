using UnityEngine;

// Listener 2: the hit sound. Lives on the Player.
public class HitSfx : MonoBehaviour
{
    Health health;
    void OnEnable() { health = GetComponent<Health>(); health.Damaged += OnHit; }
    void OnDisable() { if (health != null) health.Damaged -= OnHit; }
    void OnHit(int current, int max) => SfxPlayer.Do(Sfx.Hit);
}
