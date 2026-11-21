using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

// Put on a panel: selects its first button every time the panel opens (keyboard works at once).
public class SelectOnEnable : MonoBehaviour
{
    [SerializeField] GameObject firstButton;

    void OnEnable() => StartCoroutine(Select());

    IEnumerator Select()
    {
        yield return null;                        // wait for the EventSystem to exist
        if (EventSystem.current == null || firstButton == null) yield break;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
