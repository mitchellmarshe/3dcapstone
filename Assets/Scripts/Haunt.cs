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


    // Start is called before the first frame update
    void Start()
    {
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

    public void prepForHaunt(GameObject item, ItemActionInterface itemInfo)
    {
        lastItemObject = item;
        lastItemInterface = itemInfo;
        myController.setItemInfo(myHauntActions);
        dynamicButtonUpdaterScript.receiveItemObject(gameObject, myHauntActions);
    }

    public void possess()
    {
        //Move onto object, fix camera
        Debug.Log(lastItemObject.name);
        myController.setItemInfo(lastItemInterface);
        dynamicButtonUpdaterScript.receiveItemObject(lastItemObject, lastItemInterface);
        possessionCameraObj.transform.position = lastItemObject.transform.position;
        possessionCameraObj.SetActive(true);
        mainCam.enabled = false;
        myCharController.enabled = false;
        possessionCameraObj.transform.SetParent(lastItemObject.transform);
        
        
    }

    public void unPossess()
    {
        // return to original player controls
        mainCam.enabled = true;
        myCharController.enabled = true;
        possessionCameraObj.transform.SetParent(myPlayer.transform);
        possessionCameraObj.SetActive(false);



    }
}
