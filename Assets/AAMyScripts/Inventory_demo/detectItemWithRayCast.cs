using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detectItemWithRayCast : MonoBehaviour
{
    public Camera gameCam;
    public int rayDist;
    private Ray ray;
    public GameObject pickUpUI;
    private dialogInteractable dialogHandler;
    public GameObject inventoryCanvas;
    public int invSize;
    //private List<GameObject> inventoryList;
    private GameObject[] inventoryList;
    //private int uiSpacing = -95;
    private bool[] slotArray;
    // Start is called before the first frame update
    private int currentSize;
    public GameObject Journal;

    // Spectral Food
    public int SpectralPoints;

    void Start()
    {
        SpectralPoints = 0;
        dialogHandler = null;
        //inventoryList = new List<GameObject>();
        inventoryList = new GameObject[invSize];
        currentSize = 0;
        slotArray = new bool[invSize]; // init as all false, true means an item is in that slot
    }

    // Update is called once per frame
    void Update()
    {
        itemRayCheck();
        if (Input.GetKeyDown(KeyCode.I)) // Opens player inventory
        {
            gameCam.gameObject.GetComponentInParent<mouseLocker>().oppositeMouse();
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
            
        }
    }

    public void readItem(int num)
    {
        GameObject obj = inventoryList[num];
        string newText = obj.GetComponentInChildren<Text>().text;
        Journal.GetComponentInChildren<Text>().text = newText;
        Journal.SetActive(true);
        
    }

    public int getFood()
    {
        if (SpectralPoints > 0)
        {
            print("Hi!");
        }
        return SpectralPoints;
    }

    void itemRayCheck()
    {
        pickUpUI.SetActive(false);
        if(dialogHandler != null)
        {
            dialogHandler.setInZone(false);
            dialogHandler = null;
        }
        Vector3 rayOrigin = gameCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit rayHit;
        if (Physics.Raycast(rayOrigin, gameCam.transform.forward, out rayHit, rayDist))
        {
            GameObject other = rayHit.collider.gameObject;
            //Debug.Log("Ray hit " + rayHit.collider.gameObject.name);
            if (other.tag == "Item")
            {
                pickUpUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (currentSize < invSize) // then there is room in the inventory for a new item
                    {
                        inventoryList[currentSize] = other;
                        currentSize += 1;
                        pickUpUI.SetActive(false);
                        other.SetActive(false);

                        Sprite tmpImage = other.GetComponent<Image>().sprite; // gets the UI sprite attacked to the item
                        int openSlot = -1;
                        //Debug.Log("check1");
                        for (int i = 0; i < invSize; i++)
                        {
                            if (!slotArray[i])
                            {
                                openSlot = i;
                                i = invSize;
                            }
                        }
                        //Debug.Log("check2 " + openSlot);
                        if (openSlot != -1)
                        {
                            slotArray[openSlot] = true;
                            GameObject newItemUI = inventoryCanvas.transform.Find("item_slot" + openSlot).gameObject;
                            Debug.Log("item_slot" + openSlot);
                            newItemUI.GetComponent<Image>().sprite = tmpImage;
                            newItemUI.SetActive(true);
                        }

                        
                        //int math = -100 + ((currentSize) * 64);
                        //Debug.Log("Size = " + currentSize + "  Math = " + math);
                        //newItemUI.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(math, 0, 0);
                        
                        
                    }
                    
                }
            } else if (other.tag == "NPC")
            {
                dialogHandler = other.GetComponent<dialogInteractable>();
                dialogHandler.setInZone(true);
            } else if (other.tag == "SpectralPoint")
            {
                pickUpUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SpectralPoints += 1;
                    other.SetActive(false);
                    pickUpUI.SetActive(false);
                    //foodSlider.value = SpectralPoints / maxSpectralPoints;
                }
                }

        }
        //Debug.DrawRay(rayOrigin, gameCam.transform.forward * rayDist, Color.green);
    }
}
