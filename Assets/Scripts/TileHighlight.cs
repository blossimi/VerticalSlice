using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileHighlight : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    private SpriteRenderer render;

    void Start()
    {
        gameOpen();
    }

    private void OnMouseEnter()
    {
        Active();
    }

    private void OnMouseExit()
    {
        Inactive();
    }

    void gameOpen()
    {
        foreach (Transform child in transform)
        {
            highlight = child.gameObject;
            render = child.GetComponent<SpriteRenderer>();
        }
        render.sortingOrder = 1;
        highlight.SetActive(false);
    }

    void Active()
    {
        highlight.SetActive(true);
    }

    void Inactive()
    {
        highlight.SetActive(false);
    }
}
