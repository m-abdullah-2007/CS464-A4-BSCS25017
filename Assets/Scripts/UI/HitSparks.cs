using UnityEngine;

// Listener 3: one burst of sparks. Lives on the Player; the ParticleSystem is a child.
public class HitSparks : MonoBehaviour
{
    [SerializeField] ParticleSystem sparks;
    Health health;
    void OnEnable() { health = GetComponent<Health>(); health.Damaged += OnHit; }
    void OnDisable() { if (health != null) health.Damaged -= OnHit; }
    void OnHit(int current, int max) { if (sparks != null) sparks.Play(); }
}
