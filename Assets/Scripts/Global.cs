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
    private float npcResponsetimer;
    private float npcResponseBuffer = 0.4f;
    public MissionHandler missionHandler;
    //public bool inMenus;

    public int points;

    public bool platform;
    public int height;
    public int width;

    public int currentScene;
    public int startScene;
    public int mainScene;

    public bool tutorial;

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
        startScene = 1;
        mainScene = 2;

        tutorial = true;
    }

    void Update()
    {
        height = Screen.height;
        width = Screen.width;
        if (npcResponsetimer > 0)
        {
            npcResponsetimer -= Time.deltaTime;
        } else
        {
            npcResponsetimer = 0;
        }
        checkInteractTasks();

        
    }

    public bool canISpeak()
    {
        if(npcResponsetimer <= 0)
        {
            npcResponsetimer += 0.2f;
            return true;
        }
        return false;
    }

    public void checkInteractTasks()
    {
        
        if (missionHandler.task1Type == 2)
        {
            if (itemInfo != null && itemInfo.GetType().ToString() == missionHandler.task1Name)
            {
                missionHandler.completeTask(1);
            }
        }
        if (missionHandler.task2Type == 2)
        {
            if (itemInfo != null && itemInfo.GetType().ToString() == missionHandler.task2Name)
            {
                missionHandler.completeTask(2);
            }
        }
        if (missionHandler.task3Type == 2)
        {
            if (itemInfo != null && itemInfo.GetType().ToString() == missionHandler.task3Name)
            {
                missionHandler.completeTask(3);
            }
        }
    }
}
