using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// The only script that loads a scene. Index 0 = MainMenu, 1-5 = levels, 6 = Credits.
public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Image bar;
    [SerializeField] int creditsIndex = 6;

    static bool loading;

    void Awake() { loading = false; if (loadingPanel) loadingPanel.SetActive(false); }

    public void LoadNext()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        Load(i >= creditsIndex ? 0 : i + 1);     // wraps to the menu after Credits
    }
    public void Reload() => Load(SceneManager.GetActiveScene().buildIndex);
    public void LoadMenu() => Load(0);

    public void Load(int index)
    {
        if (loading) return;
        Time.timeScale = 1f;                      // statics survive a load: put time back
        StartCoroutine(Go(index));
    }

    IEnumerator Go(int index)
    {
        loading = true;
        if (loadingPanel) loadingPanel.SetActive(true);
        AsyncOperation op = SceneManager.LoadSceneAsync(index);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            if (bar) bar.fillAmount = Mathf.Clamp01(op.progress / 0.9f);
            yield return null;
        }
        if (bar) bar.fillAmount = 1f;
        yield return new WaitForSecondsRealtime(0.2f);   // let the bar be seen at 100%
        op.allowSceneActivation = true;
    }
}
