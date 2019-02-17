using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Note: This script is going to be more of the system for dialogue.
// Probably should change the name and variables around.

public class Dialogue : MonoBehaviour
{
    public Text decision1;
    public Text decision2;
    public Text decision3;
    public Text decision4;
    // Note: make into array

    public Text dialogue;

    private Global global;
    private GameObject dialoguePanel;
    private bool oldHaunted;

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
        dialoguePanel = GameObject.Find("Dialogue Panel");

        oldHaunted = true;
        DialogueDisplay();

        SetDecisionsDemo();
    }

    // Update is called once per frame
    void Update()
    {
        DialogueDisplay();

        if (global.decided)
        {
            if (global.action == Global.Action.One)
            {
                Dialogue1Demo();
            }

            if (global.action == Global.Action.Two)
            {
                Dialogue2Demo();
            }

            if (global.action == Global.Action.Three)
            {
                Dialogue3Demo();
            }

            if (global.action == Global.Action.Four)
            {
                Dialogue4Demo();
            }

            // Reference Controller.
            global.decided = false;
            global.action = Global.Action.None;
        }
    }

    // Note: activate dialogue display based on haunt.
    public void DialogueDisplay()
    {
        if (oldHaunted != global.haunted) {
            dialoguePanel.SetActive(global.haunted);
            oldHaunted = global.haunted;
        }
    }

    // Note: example of dialogue logic for 4 choices.
    // We need to encorporate this either into split scripts, with tag ID
    // Or a node system, or some easy way to set up varied dialogue/action.
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
