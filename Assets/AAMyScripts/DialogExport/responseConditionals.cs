using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class responseConditionals : MonoBehaviour
{

    public GameObject character;
    public GameObject NPC;
    public GameObject currentCanvas;
    public GameObject passCanvas;
    public GameObject failCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Used on button click to decide if you can perform an action costing food
    public void haveEnoughSpectralPoints(int cost)
    {
        detectItemWithRayCast rayScript = character.GetComponent<detectItemWithRayCast>();

        int currentFood = rayScript.getFood();

        if(cost > currentFood)
        {
            setupFailCanvas();
        } else
        {
            currentFood -= cost;
            rayScript.setFood(currentFood);
            setupPassCanvas();
        }
    }

    public void hasCorrectItem(string itemName)
    {
        detectItemWithRayCast rayScript = character.GetComponent<detectItemWithRayCast>();

        string[] names = rayScript.getItemNames();
        bool hasItem = false;
        int index = 0;
        while (!hasItem && index < names.Length)
        {
            if(names[index] != null && names[index] == itemName)
            {
                hasItem = true;
            }
            index++;
        }

        if (hasItem)
        {
            setupPassCanvas();
        } else
        {
            setupFailCanvas();
        }
    }

    void setupPassCanvas()
    {
        passCanvas.SetActive(true);
        currentCanvas.SetActive(false);
        NPC.GetComponent<dialogInteractable>().RegisterNewCanvas(passCanvas);

    }

    void setupFailCanvas()
    {
        failCanvas.SetActive(true);
        currentCanvas.SetActive(false);
        NPC.GetComponent<dialogInteractable>().RegisterNewCanvas(failCanvas);
    }
}
