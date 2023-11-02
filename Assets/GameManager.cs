using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip partyMusic;
    [SerializeField] private AudioSource audioSource;


    private TextDisplay textDisplay;

    private string[][] prompts =
    {
        new string[]
        {
            "where am i?", "how long have i been asleep?", "I recognize this house, i need to find the key....",
            "... the only light i have is this candle, i cant let it go out!"
        },
        new string[] { "i found the key", "i need to get inside before these stupid busters catch me" },
        new string[]
        {
            "i made it inside, i hear music!", "wheres the attic key?",
            "i need to watch out for these open windows and wind gusts...."
        },
        new string[] { "got the key", "lets get to the attic!!!" },
        new string[] { "i made it!", "all of my friends are here", "lets dance!" }
    };

    private int code = 0;

    void Start()
    {
        Time.timeScale = 1f;
        code = 0;
        Debug.Log("time scale: " + Time.timeScale);
        Debug.Log("Initializing game");
        StartCoroutine(FadeIn());
        textDisplay = FindObjectOfType<TextDisplay>();
        textDisplay.DisplayTexts(prompts[code]);
        audioSource.clip = gameMusic;
        audioSource.volume = 0f;
        audioSource.Play();
        StartCoroutine(FadeInMusic(2));
    }


    private IEnumerator FadeInMusic(float duration)
    {
        Debug.Log("Initializing music fade in");
        float startVolume = 0f;
        float endVolume = 0.5f;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, currentTime / duration);
            yield return null;
        }

        audioSource.volume = endVolume;
    }

    public void DisplayPrompt()
    {
        code++;
        textDisplay.DisplayTexts(prompts[code]);
        Debug.Log("Code for prompt: " + code);
    }


    private IEnumerator FadeIn()
    {
        fadeImage.CrossFadeAlpha(1, 0, true);
        yield return new WaitForSeconds(1);
        fadeImage.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1);
    }

    public void Party()
    {
        audioSource.Stop();
        audioSource.clip = partyMusic;
        audioSource.Play();
    }

    public void GameOver()
    {
        audioSource.Pause();
        FindObjectOfType<UI>().GameOver();
        Time.timeScale = 0f;
    }


    public void TryAgain()
    {
        StopAllCoroutines();
        Debug.Log("restarting game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void Quit()
    {
        Debug.Log("quitting game");
        Application.Quit();
    }
    
    public void MainMenu()
    {
        Debug.Log("going to main menu");
        SceneManager.LoadScene(0);
    }
}