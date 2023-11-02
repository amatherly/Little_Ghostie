using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text displayText;

    [SerializeField]
    private float displayDuration = 3f;

    [SerializeField]
    private float fadeDuration = 0.5f; 

    public void DisplayTexts(string[] texts)
    {
        StartCoroutine(DisplayTextsCoroutine(texts));
    }

    private IEnumerator DisplayTextsCoroutine(string[] texts)
    {
        foreach (string text in texts)
        {
            displayText.text = text;
            displayText.CrossFadeAlpha(0, 0, true);
            
            displayText.CrossFadeAlpha(1, fadeDuration, false);
            yield return new WaitForSeconds(fadeDuration);
            
            yield return new WaitForSeconds(displayDuration);
            
            displayText.CrossFadeAlpha(0, fadeDuration, false);
            yield return new WaitForSeconds(fadeDuration);
        }
        
        displayText.text = "";
    }
}