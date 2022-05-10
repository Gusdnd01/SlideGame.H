using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject button;
    private float fadeAlpha = 0;

    public void Button()
    {
        button.SetActive(false);
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        while (fadeAlpha <= 1f)
        {
            fadeAlpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeAlpha);
            if (fadeAlpha >= 1f)
            {
                SceneManager.LoadScene("LoadingScene");
            }
        }
    }
}
