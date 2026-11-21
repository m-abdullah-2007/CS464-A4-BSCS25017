using UnityEngine;

public class HitSparks : MonoBehaviour
{
    [SerializeField] ParticleSystem sparks;
    Health health;
    void OnEnable()
    {
        health = GetComponentInParent<Health>();
        if (health != null) health.Damaged += OnHit;
    }
    void OnDisable() { if (health != null) health.Damaged -= OnHit; }
    void OnHit(int current, int max) { if (sparks != null) sparks.Play(); }
}