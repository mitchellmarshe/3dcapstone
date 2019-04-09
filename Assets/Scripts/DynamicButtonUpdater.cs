using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Extension to Actions.cs

public class DynamicButtonUpdater : MonoBehaviour
{
    [Header("Scripts")]
    public Global global;

    [Header("Buttons")]
    public Button action1Button;
    public Button action2Button;
    public Button action3Button;
    public Button action4Button;

    public Image action1Image;
    public Image action2Image;
    public Image action3Image;
    public Image action4Image;

    public Text action1Text;
    public Text action2Text;
    public Text action3Text;
    public Text action4Text;

    [Header("Icons")]
    public Image selectorIcon;
    public Text selectorText;
    public Sprite selectorIconDefault;

    public Sprite[] icons1;
    public Sprite[] icons2;
    public Sprite[] icons3;
    public Sprite[] icons4;

    void Start()
    {
 
    }

    void Update()
    {

    }

    // This is called in 2 cases
    // 1. the player is looking/selected an object and will now display the option to haunt
    // 2. The player chose to haunt an object
    public void receiveItemObject(GameObject item, ItemActionInterface itemInfo)
    {
        selectorText.text = item.name;

        global.itemInfo = itemInfo;

        enableAllButtons();
        setStates(global.itemInfo.states);
        string[] names = itemInfo.getActionNames();
        action1Text.text = names[0];

        if (names[0] == "Open2")
        {
            action1Image.sprite = icons1[0];
        }
        else if (names[0] == "Close2")
        {
            action1Image.sprite = icons1[1];
        }
        else if (names[0] == "Open1")
        {
            action1Image.sprite = icons1[2];
        }
        else if (names[0] == "Close1")
        {
            action1Image.sprite = icons1[3];
        }
        else if (names[0] == "Burst")
        {
            action1Image.sprite = icons1[4];
        }
        else if (names[0] == "Power" 
            || names[0] == "On/Off")
        {
            action1Image.sprite = icons1[5];
        }
        else if (names[0] == "Slam" || 
            names[0] == "Haunt Call" || 
            names[0] == "Spookify")
        {
            action1Image.sprite = icons1[6];
        }
        else if (names[0] == "Crumble")
        {
            action1Image.sprite = icons1[7];
        }
        else if (names[0] == "Random Pitch")
        {
            action1Image.sprite = icons1[8];
        }
        else if (names[0] == "Distort")
        {
            action1Image.sprite = icons1[9];
        }

        action2Text.text = names[1];

        if (names[1] == "Smoke")
        {
            action2Image.sprite = icons2[0];
        }
        else if (names[1] == "Sound")
        {
            action2Image.sprite = icons2[1];
        }
        else if (names[1] == "Volume Up")
        {
            action2Image.sprite = icons2[2];
        }
        else if (names[1] == "Overload*")
        {
            action2Image.sprite = icons2[3];
        }

        action3Text.text = names[2];

        if (names[2] == "Back...")
        {
            action3Image.sprite = icons3[0];
        }

        action4Text.text = names[3];

        if (names[3] == "Special")
        {
            action4Image.sprite = icons4[0];
        }
        else if (names[3] == "Volume Down")
        {
            action4Image.sprite = icons4[1];
        }

        global.selectedItem = item;

        Image tmpImage = item.GetComponentInChildren<Image>();
        if (tmpImage == null || tmpImage.sprite == null)
        {
            selectorIcon.sprite = selectorIconDefault;
        }
        else
        {
            selectorIcon.sprite = tmpImage.sprite;
        }
    }

    public void enableAllButtons()
    {
        //Debug.Log("enabling all buttons");
        setNormalButton(1);
        setNormalButton(2);
        setNormalButton(3);
        setNormalButton(4);
    }

    private void setDisabledButton(int num)
    {
        //Debug.Log("Setting Disabled button!");
        if(num == 1)
        {
            action1Button.animator.ResetTrigger(action1Button.animationTriggers.normalTrigger);
            action1Button.animator.ResetTrigger(action1Button.animationTriggers.pressedTrigger);
            action1Button.animator.ResetTrigger(action1Button.animationTriggers.disabledTrigger);
            action1Button.animator.ResetTrigger(action1Button.animationTriggers.highlightedTrigger);
            action1Button.animator.SetTrigger(action1Button.animationTriggers.disabledTrigger);
            action1Button.interactable = false;
        } else if (num == 2)
        {
            action1Button.animator.ResetTrigger(action2Button.animationTriggers.normalTrigger);
            action2Button.animator.ResetTrigger(action2Button.animationTriggers.pressedTrigger);
            action2Button.animator.ResetTrigger(action2Button.animationTriggers.disabledTrigger);
            action2Button.animator.ResetTrigger(action2Button.animationTriggers.highlightedTrigger);
            action2Button.animator.SetTrigger(action2Button.animationTriggers.disabledTrigger);
            action2Button.interactable = false;
        }
        else if (num == 3)
        {
            action3Button.animator.ResetTrigger(action3Button.animationTriggers.normalTrigger);
            action3Button.animator.ResetTrigger(action3Button.animationTriggers.pressedTrigger);
            action3Button.animator.ResetTrigger(action3Button.animationTriggers.disabledTrigger);
            action3Button.animator.ResetTrigger(action3Button.animationTriggers.highlightedTrigger);
            action3Button.animator.SetTrigger(action3Button.animationTriggers.disabledTrigger);
            action3Button.interactable = false;
        }
        else if (num == 4)
        {
            action4Button.animator.ResetTrigger(action4Button.animationTriggers.normalTrigger);
            action4Button.animator.ResetTrigger(action4Button.animationTriggers.pressedTrigger);
            action4Button.animator.ResetTrigger(action4Button.animationTriggers.disabledTrigger);
            action4Button.animator.ResetTrigger(action4Button.animationTriggers.highlightedTrigger);
            action4Button.animator.SetTrigger(action4Button.animationTriggers.disabledTrigger);
            action4Button.interactable = false;
        }
    }

    public void setNormalButton(int num)
    {
        if (num == 1)
        {
            action1Button.interactable = true;
            action1Button.animator.ResetTrigger(action1Button.animationTriggers.normalTrigger);
            action1Button.animator.ResetTrigger(action1Button.animationTriggers.pressedTrigger);
            action1Button.animator.ResetTrigger(action1Button.animationTriggers.disabledTrigger);
            action1Button.animator.ResetTrigger(action1Button.animationTriggers.highlightedTrigger);
            action1Button.animator.SetTrigger(action1Button.animationTriggers.pressedTrigger);
            //action1Button.animator.SetTrigger(action1Button.animationTriggers.pressedTrigger);
            
            

        }
        else if (num == 2)
        {
            action2Button.interactable = true;
            action2Button.animator.ResetTrigger(action2Button.animationTriggers.normalTrigger);
            action2Button.animator.ResetTrigger(action2Button.animationTriggers.pressedTrigger);
            action2Button.animator.ResetTrigger(action2Button.animationTriggers.disabledTrigger);
            action2Button.animator.ResetTrigger(action2Button.animationTriggers.highlightedTrigger);
            action2Button.animator.SetTrigger(action2Button.animationTriggers.pressedTrigger);
            //action2Button.animator.SetTrigger(action2Button.animationTriggers.pressedTrigger);
            
            
        }
        else if (num == 3)
        {
            action3Button.interactable = true;
            action3Button.animator.ResetTrigger(action3Button.animationTriggers.normalTrigger);
            action3Button.animator.ResetTrigger(action3Button.animationTriggers.pressedTrigger);
            action3Button.animator.ResetTrigger(action3Button.animationTriggers.disabledTrigger);
            action3Button.animator.ResetTrigger(action3Button.animationTriggers.highlightedTrigger);
            action3Button.animator.SetTrigger(action3Button.animationTriggers.pressedTrigger);
            
            //action3Button.animator.SetTrigger(action3Button.animationTriggers.pressedTrigger);


        }
        else if (num == 4)
        {
            action4Button.interactable = true;
            action4Button.animator.ResetTrigger(action4Button.animationTriggers.normalTrigger);
            action4Button.animator.ResetTrigger(action4Button.animationTriggers.pressedTrigger);
            action4Button.animator.ResetTrigger(action4Button.animationTriggers.disabledTrigger);
            action4Button.animator.ResetTrigger(action4Button.animationTriggers.highlightedTrigger);
            action4Button.animator.SetTrigger(action4Button.animationTriggers.pressedTrigger);
            
            //action4Button.animator.SetTrigger(action4Button.animationTriggers.pressedTrigger);


        }
    }

    public bool[] getStates()
    {
        bool[] result = new bool[4] {action1Button.interactable, action2Button.interactable, action3Button.interactable, action4Button.interactable};
        return result;
    }

    public void setStates(bool[] newStates)
    {
        for (int i = 0; i < 4; i++)
        {
            if (newStates[i])
            {
                setNormalButton(i+1);
            }
            else
            {
                setDisabledButton(i+1);
            }
        }
    }

}
