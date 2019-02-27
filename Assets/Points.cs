using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{

    private Global global;
    private Text pointsText;
    private Slider havokSlider;

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
        pointsText = GameObject.Find("GUI/HUD/Points").GetComponent<Text>();
        havokSlider = GameObject.Find("GUI/HUD/Spectral Energy").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = "" + global.points + " Points";
        havokSlider.value = global.points;
    }
}
