using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private Image image;
    private float fadeAlpha = 1f;

    void Start()
    {
        StartCoroutine(Fadeout()); 
    }

    IEnumerator Fadeout()
    {
        while(fadeAlpha >= 0f)
        {
            fadeAlpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeAlpha);
        }
    }
}
