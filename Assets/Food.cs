using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public Slider foodSlider;
    public detectItemWithRayCast script;
    private int max = 5;

    // Start is called before the first frame update
    void Start()
    {
        foodSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        foodSlider.value = (float) script.getFood() / max;
    }
}
