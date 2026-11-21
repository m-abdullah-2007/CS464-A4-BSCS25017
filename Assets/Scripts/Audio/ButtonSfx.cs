using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSfx : MonoBehaviour
{
    void Awake() => GetComponent<Button>().onClick.AddListener(() => SfxPlayer.Do(Sfx.Button));
}
