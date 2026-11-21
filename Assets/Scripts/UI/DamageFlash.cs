using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Listener 4: a red flash over the screen. Lives on the HUD.
public class DamageFlash : MonoBehaviour
{
    [SerializeField] Image flash;
    Health health;

    void OnEnable()
    {
        GameObject p = GameObject.FindWithTag("Player");
        health = p != null ? p.GetComponent<Health>() : null;
        if (health != null) health.Damaged += OnHit;
    }
    void OnDisable() { if (health != null) health.Damaged -= OnHit; }

    void OnHit(int current, int max) { StopAllCoroutines(); StartCoroutine(Flash()); }

    IEnumerator Flash()
    {
        for (float t = 0f; t < 0.3f; t += Time.unscaledDeltaTime)
        {
            Color c = Pal.Flash; c.a *= 1f - t / 0.3f; flash.color = c;
            yield return null;
        }
        flash.color = Color.clear;
    }
}
