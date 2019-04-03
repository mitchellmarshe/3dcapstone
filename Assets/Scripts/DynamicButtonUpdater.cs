using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicButtonUpdater : MonoBehaviour
{
    Global global;
    public Button action1Button;
    public Button action2Button;
    public Button action3Button;
    public Button action4Button;

    public Text action1Text;
    public Text action2Text;
    public Text action3Text;
    public Text action4Text;
    public Image selectorIcon;
    public Text selectorText;
    // Start is called before the first frame update
    void Start()
    {
        
        global = GameObject.Find("Global").GetComponent<Global>();
        if (!global.platform) // PC
        {
            action1Button = GameObject.Find("PC Actions/Action 1 Button").GetComponent<Button>();
            action2Button = GameObject.Find("PC Actions/Action 2 Button").GetComponent<Button>();
            action3Button = GameObject.Find("PC Actions/Action 3 Button").GetComponent<Button>();
            action4Button = GameObject.Find("PC Actions/Action 4 Button").GetComponent<Button>();

            action1Text = GameObject.Find("PC Actions/Action 1 Button/Action 1 Text").GetComponent<Text>();
            action2Text = GameObject.Find("PC Actions/Action 2 Button/Action 2 Text").GetComponent<Text>();
            action3Text = GameObject.Find("PC Actions/Action 3 Button/Action 3 Text").GetComponent<Text>();
            action4Text = GameObject.Find("PC Actions/Action 4 Button/Action 4 Text").GetComponent<Text>();

            selectorIcon = GameObject.Find("PC Actions/Selection Icon").GetComponent<Image>();
            selectorText = GameObject.Find("PC Actions/Selection Icon/Selection Text").GetComponent<Text>();
        }
        else // MOBILE
        {
            action1Button = GameObject.Find("Mobile Actions/Action 1 Button").GetComponent<Button>();
            action2Button = GameObject.Find("Mobile Actions/Action 2 Button").GetComponent<Button>();
            action3Button = GameObject.Find("Mobile Actions/Action 3 Button").GetComponent<Button>();
            action4Button = GameObject.Find("Mobile Actions/Action 4 Button").GetComponent<Button>();

            action1Text = GameObject.Find("Mobile Actions/Action 1 Button/Action 1 Text").GetComponent<Text>();
            action2Text = GameObject.Find("Mobile Actions/Action 2 Button/Action 2 Text").GetComponent<Text>();
            action3Text = GameObject.Find("Mobile Actions/Action 3 Button/Action 3 Text").GetComponent<Text>();
            action4Text = GameObject.Find("Mobile Actions/Action 4 Button/Action 4 Text").GetComponent<Text>();

            selectorIcon = GameObject.Find("Mobile Actions/Selection Icon").GetComponent<Image>();
            selectorText = GameObject.Find("Mobile Actions/Selection Icon/Selection Text").GetComponent<Text>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Global.Action myAction = global.action;
        //enableAllButtons();
    }

    // This is called in 2 cases
    // 1. the player is looking/selected an object and will now display the option to haunt
    // 2. The player chose to haunt an object
    public void receiveItemObject(GameObject item, ItemActionInterface itemInfo)
    {
        //Debug.Log("iteminfo " + itemInfo);
        selectorText.text = item.name;
        global.itemInfo = itemInfo;
        enableAllButtons();
        //Debug.Log(global.itemInfo.states[1]);
        setStates(global.itemInfo.states);
        string[] names = itemInfo.getActionNames();
        action1Text.text = names[0];
        action2Text.text = names[1];
        action3Text.text = names[2];
        action4Text.text = names[3];
        global.selectedItem = item;
        Image tmpImage = item.GetComponentInChildren<Image>();
        if (tmpImage == null || tmpImage.sprite == null)
        {
            //Debug.Log(selectorText.text + " has no associated icon Image");
        }
        else
        {
            selectorIcon = tmpImage;
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
        bool[] result = new bool[4] {action1Button.interactable, action2Button.interactable , action3Button.interactable , action4Button.interactable };
        return result;
    }

    public void setStates(bool[] newStates)
    {
        for(int i = 0; i < 4; i++)
        {
            if (newStates[i])
            {
                setNormalButton(i+1);
            } else
            {
                setDisabledButton(i+1);
            }
        }
    }

}
