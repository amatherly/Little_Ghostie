using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private TextDisplay textDisplay;
    private string[][] prompts =
    {
        new string[] { "where am i?", "how long have i been asleep?", "I recognize this house, i need to find the key....", "... the only light i have is this candle, i cant let it go out!" },
        new string[] { "i found the key", "i need to get inside before these stupid busters catch me" },
        new string[] { "i made it inside, i hear music!", "ill be safe in the attic", "i must find the key....", "i need to watch out for these open windows and wind gusts...." },
        new string[] { "i made it!", "all of my friends are here", "lets dance!" }
    };

    private int code = 0;

    void Start()
    {
        StartCoroutine(FadeIn());
        textDisplay = FindObjectOfType<TextDisplay>();
        textDisplay.DisplayTexts(prompts[code]);
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
        yield return new WaitForSeconds(2);
        fadeImage.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(3);
    }
}