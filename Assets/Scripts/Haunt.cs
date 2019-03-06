using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haunt : MonoBehaviour
{
    private HauntActions myHauntActions;
    private DynamicButtonUpdater dynamicButtonUpdaterScript;
    private ItemActionInterface lastItemInterface;
    public GameObject lastItemObject;
    private Controller myController;
    private GameObject myPlayer;
    private GameObject possessionCameraObj;
    private Camera possessionCam;
    private Camera mainCam;
    private CharacterController myCharController;
    private Queue<ItemActionInterface> previousMenus = new Queue<ItemActionInterface>();
    private Global myGlobal;


    // Start is called before the first frame update
    void Start()
    {
        myGlobal = GameObject.Find("Global").GetComponent<Global>();
        myPlayer = GameObject.Find("Player");
        myCharController = myPlayer.GetComponent<CharacterController>();
        possessionCameraObj = GameObject.Find("possessionCamera");
        mainCam = GameObject.Find("Camera").GetComponent<Camera>();
        possessionCam = possessionCameraObj.GetComponent<Camera>();
        possessionCameraObj.SetActive(false);
        myController = gameObject.GetComponent<Controller>();
        myHauntActions = gameObject.GetComponent<HauntActions>();
        dynamicButtonUpdaterScript = gameObject.GetComponent<DynamicButtonUpdater>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Sets up the haunt/unhaunt actions buttons when initially looking at an object
    //Also stores the object and the implemented abstract ItemActionInterface
    // associated with that object
    public void prepForHaunt(GameObject item, ItemActionInterface itemInfo)
    {
        lastItemObject = item;
        lastItemInterface = itemInfo;
        myController.setItemInfo(myHauntActions);
        
        dynamicButtonUpdaterScript.receiveItemObject(gameObject, myHauntActions);
        dynamicButtonUpdaterScript.setStates(myHauntActions.states);
    }

    //This is called to progress down a tree of actions
    public void goFowardAHaunt(ItemActionInterface info)
    {
        previousMenus.Enqueue(lastItemInterface);
        lastItemInterface = info;
        
        myController.setItemInfo(info);
        
        dynamicButtonUpdaterScript.receiveItemObject(lastItemObject, info);
        dynamicButtonUpdaterScript.setStates(info.states);
    }

    //This returns to previous menus in the tree
    public void goBackAHaunt()
    {
        if (previousMenus.Count > 0)
        {
            lastItemInterface = previousMenus.Dequeue();
            myController.setItemInfo(lastItemInterface);
            Debug.Log(lastItemInterface + "goBackAHaunt");
            dynamicButtonUpdaterScript.receiveItemObject(lastItemObject, lastItemInterface);
            dynamicButtonUpdaterScript.setStates(lastItemInterface.states);
        }
    }

    //This handles the camera switching and GUI updating
    public void possess()
    {
        //Move onto object, fix camera

        Debug.Log(lastItemObject.name);
        previousMenus.Clear();
        
        myController.setItemInfo(lastItemInterface);
        dynamicButtonUpdaterScript.receiveItemObject(lastItemObject, lastItemInterface);
        dynamicButtonUpdaterScript.setStates(lastItemInterface.states);
        possessionCameraObj.transform.position = lastItemObject.transform.position;
        possessionCameraObj.SetActive(true);
        mainCam.enabled = false;
        myCharController.enabled = false;
        possessionCameraObj.transform.SetParent(lastItemObject.transform);
        
        
    }

    //This returns the player to normal controls/camera
    public void unPossess()
    {
        // return to original player controls
        myGlobal.possessing = false;
        //myGlobal.possesMove = false;
        previousMenus.Clear();
        mainCam.enabled = true;
        myCharController.enabled = true;
        possessionCameraObj.transform.SetParent(myPlayer.transform);
        possessionCameraObj.SetActive(false);
        


    }
}
