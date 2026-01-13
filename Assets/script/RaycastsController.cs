using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastsController : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [Range(0.99f, 1f)]
    public float clickableThreshold = 0.9f;
 
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        UpdateClickableState();
    }

    private void Update()
    {
        UpdateClickableState();
    }

    private void UpdateClickableState()
    {
        // alpha‚ª1‚É‹ß‚¯‚ê‚ÎG‚ê‚éA‚»‚êˆÈŠO‚ÍG‚ê‚È‚¢
        bool canClick = canvasGroup.alpha >= clickableThreshold;

        canvasGroup.blocksRaycasts = canClick;
        canvasGroup.interactable = canClick;
    }
}
