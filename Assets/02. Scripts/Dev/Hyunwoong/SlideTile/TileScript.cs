using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Vector2         targetPosition;
    private Vector2        correctPosition;
    private SpriteRenderer spriteRenderer;

    public int number;

    public bool isRightPlace;

    private void Awake()
    {
        targetPosition = transform.position;
        correctPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, targetPosition, 0.05f);

        if (targetPosition == correctPosition)
        {
            spriteRenderer.color = Color.green;
            isRightPlace = true;
        }
        else
        {
            spriteRenderer.color = Color.white;
            isRightPlace = false ;
        }
    }
}
