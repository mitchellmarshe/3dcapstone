using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haunt : MonoBehaviour
{
    private HauntActions myHauntActions;
    private DynamicButtonUpdater dynamicButtonUpdaterScript;
    private ItemActionInterface lastItemInterface;
    private GameObject lastItemObject;
    private Controller myController;
    private GameObject myPlayer;
    private GameObject possessionCameraObj;
    private Camera possessionCam;


    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameObject.Find("Player");
        possessionCameraObj = GameObject.Find("possessionCamera");
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
        myPlayer.SetActive(false);
        
    }

    public void unPossess()
    {
        // return to original player controls
        myPlayer.SetActive(true);
        possessionCameraObj.SetActive(false);
        
    }
}
