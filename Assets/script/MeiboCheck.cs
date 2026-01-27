using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MeiboCheck : MonoBehaviour
{
    SpriteRenderer sr;

    public GameObject infoPanel;
    public TextMeshProUGUI nameText;
    public Image iconImage;

    public float rotationSpeed = 1f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    void OnMouseEnter()
    {
        if (sr != null)
            sr.color = Color.red;

        if (infoPanel != null)
            infoPanel.SetActive(true);

        if (nameText != null)
            nameText.text = "ëäêÏ";
    }

    void OnMouseOver()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }

    void OnMouseExit()
    {
        if (sr != null)
            sr.color = Color.white;

        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    void OnMouseDown()
    {
        rotationSpeed *= -1;
    }
}
