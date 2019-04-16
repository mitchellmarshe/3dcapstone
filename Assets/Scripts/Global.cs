using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Global settings/variable for the whole game!

public class Global : MonoBehaviour
{
    public bool haunted;
    public bool mouseLocked;
    public bool placingDecal;
    public bool decided;
    public bool possessing;
    public GameObject softSelected = null;
    public ItemActionInterface itemInfo;
    public GameObject hardSelected = null;
    public enum Action {None, One, Two, Three, Four};
    public Action action;
    public GameObject selectedItem;
    //public bool inMenus;

    public int points;

    public bool platform;
    public int height;
    public int width;

    public int currentScene;
    public int startScene;
    public int mainScene;

    public bool tutorial;

    // Start is called before the first frame update.
    void Awake()
    {
        #if UNITY_STANDALONE_WIN
            Debug.Log("Win");
            platform = false;
        #elif UNITY_STANDALONE_OSX
            Debug.Log("OSX");
            platform = false;
        #elif UNITY_ANDROID
            Debug.Log("Android");
            platform = true;
        #elif UNITY_IOS
            Debug.Log("IOS");
            platform = true;
        #else
            Debug.Log("Unsupported");
            platform = false;
        #endif

        height = Screen.height;
        width = Screen.width;

        itemInfo = null;
        possessing = false;
        //possesMove = false;
        haunted = false;
        mouseLocked = true;
        placingDecal = false;
        decided = false;
        action = Action.None;
        selectedItem = null;
        //inMenus = false;
        points = 0;

        currentScene = SceneManager.GetActiveScene().buildIndex;
        startScene = 0;
        mainScene = 1;

        tutorial = true;
    }

    // Update is called once per frame.
    void Update()
    {
        
    }
}
