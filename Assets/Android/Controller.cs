using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Text dialogue;
    public Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        position = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            position = touch.position;
        }
        //dialogue.text = "Position: (" + position.x + ", " + position.y + ")";
    }
}
