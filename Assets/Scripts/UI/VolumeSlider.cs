using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Sits on an ALWAYS-ACTIVE object (the Canvas root), not on the hidden pause panel.
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] AudioMixer mixer;

    void Start()
    {
        slider.minValue = 0.0001f;
        slider.maxValue = 1f;
        float v = SaveData.GetMusicVolume();
        slider.SetValueWithoutNotify(v);
        Apply(v);
        slider.onValueChanged.AddListener(OnChanged);
    }

    void OnChanged(float v) { SaveData.SetMusicVolume(v); Apply(v); }

    void Apply(float v)
    {
        if (mixer != null) mixer.SetFloat("MusicVolume", Mathf.Log10(v) * 20f);
    }
}
