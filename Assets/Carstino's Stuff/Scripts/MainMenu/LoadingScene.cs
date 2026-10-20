using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider ProgressBar;

    public void LoadScene(int sceneId)
    {
        LoadingScreen.SetActive(true);
        ProgressBar.value = 0;
        StartCoroutine(LoadSceneAsync(sceneId));
        
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);
        while (!asyncLoad.isDone)
        {
            ProgressBar.value = asyncLoad.progress;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        LoadingScreen.SetActive(false);
    }
}
