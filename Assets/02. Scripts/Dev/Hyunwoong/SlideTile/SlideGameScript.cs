using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlideGameScript : MonoBehaviour
{

    [SerializeField] private Transform    emptySpace;
    [SerializeField] private TileScript[] tiles;
    [SerializeField] private Image        image;
    [SerializeField] private GameObject   button;

    private Camera cam;
    private float fadeAlpha;
    private int emptySpaceIndex = 15;
    private bool isFinished;

    void Start()
    {
        cam = Camera.main;
        StartCoroutine(TileSlide());
        Suffle();
    }

    IEnumerator TileSlide()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 2.5f)
                {
                    Vector2 lastEmptySpacePotision = emptySpace.position;
                    TileScript thisTile = hit.transform.GetComponent<TileScript>();
                    emptySpace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySpacePotision;
                    int tileIndex = FindIndex(thisTile);
                    if(tileIndex == -1)
                    {
                        continue;
                    }
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    private void Update()
    {
        if (!isFinished)
        {
            int correctTiles = 0;
            foreach (var a in tiles)
            {
                if (a != null)
                {
                    if (a.isRightPlace)
                    {
                        correctTiles++;
                    }
                }
            }
            if (correctTiles == tiles.Length - 1)
            {
                
                isFinished = true;
                StartCoroutine(Fade());
            }
        }
    }

    public void Suffle()
    {
        if(emptySpaceIndex != 15)
        {
            var tileOn15LastPos = tiles[15].targetPosition;
            tiles[15].targetPosition = emptySpace.position;
            emptySpace.position = tileOn15LastPos;
            tiles[emptySpaceIndex] = tiles[15];
            tiles[15] = null;
            emptySpaceIndex = 15;
        }
        int invertion;
        do
        {
            for (int i = 0; i < 15; i++)
            {
                var lastPos = tiles[i].targetPosition;
                int randomIndex = Random.Range(0, 15);
                tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                tiles[randomIndex].targetPosition = lastPos;

                var tile = tiles[i];
                tiles[i] = tiles[randomIndex];
                tiles[randomIndex] = tile;
            }
            invertion = GetInversions();
        } while (invertion % 2 != 0);
        
    }
    public int FindIndex(TileScript ts)
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i] != null)
            {
                if(tiles[i] == ts)
                {
                    return i;
                }
            }
        }

        return -1;
    }

    int GetInversions()
    {
        int inversionsSum = 0;
        for(int i = 0; i < tiles.Length; i++)
        {
            int thisTileInvertion = 0;
            for (int j = i; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInvertion++;
                    }
                }
            }
            inversionsSum += thisTileInvertion;
        }
        
        return inversionsSum;
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

