using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [Header("Time to wait before loading in seconds")]
    [SerializeField] private float delayTime = 3.0f;

    [Header("Name of the main game scene")]
    [SerializeField] private string mainGameSceneName = "SampleScene";

    void Start()
    {
        StartCoroutine(LoadMainGameSceneAfterDelay());
    }

    private IEnumerator LoadMainGameSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(mainGameSceneName);
    }
}
