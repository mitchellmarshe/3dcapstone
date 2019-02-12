using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Text decision1;
    public Text decision2;
    public Text decision3;
    public Text decision4;
    // Note: make into array

    public Text dialogue;

    // Start is called before the first frame update
    void Start()
    {
        SetDecisionsDemo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDecisionsDemo()
    {
        decision1.text = "Hello";
        decision2.text = "Who are you?";
        decision3.text = "Bye";
        decision4.text = "...";
        dialogue.text = "";
    }

    public void Dialogue1Demo()
    {
        dialogue.text = "Hi!";
    }

    public void Dialogue2Demo()
    {
        dialogue.text = "I'm Mitchell, the developer.";
    }

    public void Dialogue3Demo()
    {
        dialogue.text = "Farewell!";
    }

    public void Dialogue4Demo()
    {
        dialogue.text = "...";
    }
}
