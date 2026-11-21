using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] TMP_Text[] labels;
    [SerializeField] LevelLoader loader;

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int level = i + 1;
            buttons[i].onClick.AddListener(() => loader.Load(level));
        }
    }

    void OnEnable() => Refresh();

    void Refresh()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int level = i + 1;
            bool open = SaveData.IsUnlocked(level);
            buttons[i].interactable = open;
            string second = !open ? "Locked" : SaveData.HasBest(level) ? $"Best {SaveData.GetBest(level):0.0} s" : "No time yet";
            labels[i].text = $"Level {level}\n{second}";
            labels[i].color = open ? Pal.Panel : Pal.DimText;
        }
    }
}
