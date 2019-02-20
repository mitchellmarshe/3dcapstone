using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicButtonUpdater : MonoBehaviour
{
    Global global;
    private Button action1Button;
    private Button action2Button;
    private Button action3Button;
    private Button action4Button;

    private Text action1Text;
    private Text action2Text;
    private Text action3Text;
    private Text action4Text;
    private Button selectorIcon;
    private Text selectorText;
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

            selectorIcon = GameObject.Find("PC Actions/Selection Icon").GetComponent<Button>();
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

            selectorIcon = GameObject.Find("Mobile Actions/Selection Icon").GetComponent<Button>();
            selectorText = GameObject.Find("Mobile Actions/Selection Icon/Selection Text").GetComponent<Text>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Global.Action myAction = global.action;
    }

    public void receiveItemObject(GameObject item, ItemActionInterface itemInfo)
    {
        selectorText.text = item.name;

        string[] names = itemInfo.getActionNames();
        action1Text.text = names[0];
        action2Text.text = names[1];
        action3Text.text = names[2];
        action4Text.text = names[3];
        global.selectedItem = item;
        Image tmpImage = item.GetComponentInChildren<Image>();
        if (tmpImage == null || tmpImage.sprite == null)
        {
            Debug.Log(selectorText.text + " has no associated icon Image");
        }
        else
        {
            selectorIcon.image = tmpImage;
        }
        
    }

}
