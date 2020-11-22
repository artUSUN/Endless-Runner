using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Sprite green;
    [SerializeField] private Sprite yellow;
    [SerializeField] private Sprite red;

    private Image bar;

    private void Start()
    {
        bar = GetComponent<Image>();
    }

    private void Update()
    {
        if (bar.sprite != green && bar.fillAmount > 0.5f) bar.sprite = green;
        else if (bar.sprite != yellow && 0.25f < bar.fillAmount && bar.fillAmount < 0.5f) bar.sprite = yellow;
        else if (bar.sprite != red && bar.fillAmount < 0.25f) bar.sprite = red;
    }
}
