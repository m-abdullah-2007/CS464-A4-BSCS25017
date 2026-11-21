
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int creditsIndex = 6;

    [Header("Loading UI")]
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Image loadingBar;

    public void LoadNext()
    {
        int current = SceneManager.GetActiveScene().buildIndex;

        if (current == creditsIndex)
            Go(0);
        else
            Go(current + 1);
    }

    public void Reload()
    {
        Go(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Go(0);
    }

    public void Load(int index)
    {
        Go(index);
    }

    private void Go(int index)
    {
        Time.timeScale = 1f;
        StartCoroutine(Run(index));
    }

    private IEnumerator Run(int index)
    {
        loadingPanel.SetActive(true);

        AsyncOperation operation =
            SceneManager.LoadSceneAsync(index);

        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            loadingBar.fillAmount =
                Mathf.Clamp01(operation.progress / 0.9f);

            yield return null;
        }

        loadingBar.fillAmount = 1f;

        yield return new WaitForSecondsRealtime(0.2f);

        operation.allowSceneActivation = true;
    }
}
