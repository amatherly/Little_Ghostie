using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PartyLights : MonoBehaviour
{
    [SerializeField] private Light2D[] lights;
    [SerializeField] private float flickerDuration = 0.2f;
    [SerializeField] private float flickerIntensity = 0.5f;
    [SerializeField] private Color[] colors;
    
    
    void Start()
    {
        colors = new[] { Color.cyan, Color.blue, Color.magenta, Color.yellow, Color.green };
        StartCoroutine(FlickerLights());
    }


    private IEnumerator FlickerLights()
    {
        // for each light in lights, set the color to a random color from colors
        while (true)
        {
            foreach (Light2D light in lights)
            {
                light.color = colors[Random.Range(0, colors.Length)];
                yield return new WaitForSeconds(0.4f);
            }
        }
    }
}
