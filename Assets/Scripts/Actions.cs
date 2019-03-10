using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
    [Header("Scripts")]
    public Global global;
    public DynamicButtonUpdater dynamicButtonUpdater;

    [Header("Actions")]
    public Button action1Button;
    public Button action2Button;
    public Button action3Button;
    public Button action4Button;

    private ItemActionInterface itemInfo;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // The default state of the action wheel is to have no action buttons triggered.
    public void Action0()
    {
        global.decided = false;
        global.action = Global.Action.None;
        action1Button.animator.SetTrigger("Normal");
        action2Button.animator.SetTrigger("Normal");
        action3Button.animator.SetTrigger("Normal");
        action4Button.animator.SetTrigger("Normal");
    }

    // Triggerable script for the action wheel button named Action 1.
    // When calling from the Unity Editor, set trigger to true.
    public void Action1(bool trigger)
    {
        if (action1Button.interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || trigger == true)
            {
                global.decided = true;
                global.action = Global.Action.One;

                // Button colors are private; therefore, 
                // we must control the state via animation.
                if (trigger == false)
                {
                    action1Button.animator.SetTrigger("Pressed");
                }

                // Call specialized action (based on context).
                if (itemInfo != null)
                {
                    itemInfo.callAction1();
                }
                else
                {
                    print("itemInfo null in action button1");
                }
            }

            // Reset button animations.
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                action1Button.animator.SetTrigger("Normal");
            }

            if (trigger == true)
            {
                action1Button.OnDeselect(null);
            }
        }
    }

    // See Action1() for comments.
    public void Action2(bool trigger)
    {
        if (action2Button.interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) || trigger == true)
            {
                global.decided = true;
                global.action = Global.Action.Two;

                if (trigger == false)
                {
                    action2Button.animator.SetTrigger("Pressed");
                }

                if (itemInfo != null)
                {
                    itemInfo.callAction2();
                }
            }

            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                action2Button.animator.SetTrigger("Normal");
            }

            if (trigger == true)
            {
                action2Button.OnDeselect(null);
            }
        }
    }

    // See Action1() for comments.
    public void Action3(bool trigger)
    {
        if (action3Button.interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3) || trigger == true)
            {
                global.decided = true;
                global.action = Global.Action.Three;

                if (trigger == false)
                {
                    action3Button.animator.SetTrigger("Pressed");
                }

                if (itemInfo != null)
                {
                    itemInfo.callAction3();
                } 
            }

            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                action3Button.animator.SetTrigger("Normal");
            }

            if (trigger == true)
            {
                action3Button.OnDeselect(null);
            }
        }
    }

    // See Action1() for comments.
    public void Action4(bool trigger)
    {
        if (action4Button.interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4) || trigger == true)
            {
                global.decided = true;
                global.action = Global.Action.Four;

                if (trigger == false)
                {
                    action4Button.animator.SetTrigger("Pressed");
                }

                if (itemInfo != null)
                {
                    itemInfo.callAction4();
                }
            }

            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                action4Button.animator.SetTrigger("Normal");
            }

            if (trigger == true)
            {
                action4Button.OnDeselect(null);
            }
        }
    }
}
