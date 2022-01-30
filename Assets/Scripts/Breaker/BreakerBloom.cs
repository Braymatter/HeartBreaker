using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BreakerBloom : MonoBehaviour
{
    public Renderer engineBodyRenderer;
    public float speed;
    public Color startColor, endColor;
    public float startTime;

// Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        StartCoroutine(ChangeEngineColour());
    }

    private IEnumerator ChangeEngineColour()
    {
        while (engineBodyRenderer.material.color != endColor)
        {
            engineBodyRenderer.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
    }
}

// have it take a duration and factor as arguments