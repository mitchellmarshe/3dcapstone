using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IgnoreAlpha : MonoBehaviour
{
    public Image image;
    public float alphaThreshold;

    void Start()
    {
        image.alphaHitTestMinimumThreshold = alphaThreshold;
    }
}
