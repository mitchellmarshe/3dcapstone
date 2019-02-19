using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class joystickTest : MonoBehaviour
{
    private RectTransform movementStickArea;
    private RectTransform lookStickArea;
    public GameObject player;
    public Camera playerCam;
    enum sticks { moveStick, lookStick, noStick};
    //private List<Touch> storedTouches;
    private Touch? moveTouch = null;
    private Touch? lookTouch = null;

    // Start is called before the first frame update
    void Start()
    {
        // I'm 90% sure the rect areas need to be grabbed at the start and not selected from
        // the editor because they get scaled on start to mobile and a public var didn't
        //seem to update that info
        movementStickArea = GameObject.FindGameObjectWithTag("moveArea").GetComponent<RectTransform>();
        lookStickArea = GameObject.FindGameObjectWithTag("lookArea").GetComponent<RectTransform>();
        //storedTouches = new List<Touch>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Touch myTouch in Input.touches)
        {
            if(myTouch.phase == TouchPhase.Began)
            {
                sticks myStick = determineStick(myTouch);
                if (myStick == sticks.moveStick)
                {
                    //storedTouches.Add(myTouch);
                    Debug.Log("MoveStick Touch Stored");
                    moveTouch = myTouch;

                } else if (myStick == sticks.lookStick)
                {
                    //storedTouches.Add(myTouch);
                    Debug.Log("lookStick Touch Stored");
                    lookTouch = myTouch;
                }
            }else if (myTouch.phase == TouchPhase.Moved)
            {
                if(moveTouch != null && myTouch.fingerId == ((Touch)moveTouch).fingerId)
                {
                    Debug.Log("MoveTouch has moved");
                    Vector2 myDelta = myTouch.deltaPosition;
                    Text debugger = GameObject.Find("Debugger").GetComponent<Text>();
                    debugger.text = "X: " + myDelta.x + "   y: " + myDelta.y;
                    // reference myTouch NOT moveTouch or lookTouch because their values are not updated


                }
                else if (lookTouch != null && myTouch.fingerId == ((Touch)lookTouch).fingerId)
                {
                    Debug.Log("LookTouch has moved");
                    Transform tmpTransform = player.GetComponent<Transform>();
                    Vector2 myDelta = myTouch.deltaPosition;
                    //Debug.Log("X:  " + myDelta.x + "   y: " + myDelta.y);
                    Text debugger = GameObject.Find("Debugger").GetComponent<Text>();
                    debugger.text = "X: " + myDelta.x + "   y: " + myDelta.y;
                    //player.GetComponent<Transform>().Rotate(myDelta.x, myDelta.y, 0);

                }


            }
            else if(myTouch.phase == TouchPhase.Ended)
            {
                if (moveTouch != null && myTouch.fingerId == ((Touch)moveTouch).fingerId)
                {
                    Debug.Log("moveStick Touch removed");
                    moveTouch = null;
                }
                else if (lookTouch != null && myTouch.fingerId == ((Touch)lookTouch).fingerId)
                {
                    Debug.Log("lookStick Touch removed");
                    lookTouch = null;
                }
                    /*
                    for(int i = 0; i < storedTouches.Count; i++)
                    {
                        if (storedTouches[i].fingerId == myTouch.fingerId)
                        {
                            storedTouches.RemoveAt(i);
                            i = storedTouches.Count;
                            //Debug.Log("touch Removed from storage");
                        }
                    } */
                }
        }

        /*
         * This code will not work because the touch is being stored
         * by value and not by reference so the data is never updated.
         * Possible fixes is moving back into all touch loop and ID'ing
         * if a touch has the same fingerID OR updating based on finger ID
         * and doing the translate here
         * 
         * I'm just going to do it all above
         * 
        if(moveTouch != null)
        {
            if(((Touch)moveTouch).phase == TouchPhase.Moved)
            {
                Debug.Log("move has moved");
            }
        }

        if(lookTouch != null)
        {
            if (((Touch)lookTouch).phase == TouchPhase.Moved)
            {
                Debug.Log("look has moved");
            }
        }
        */
    }

    sticks determineStick(Touch myTouch)
    {
        sticks result = sticks.noStick;
        
        if(RectTransformUtility.RectangleContainsScreenPoint(movementStickArea, myTouch.position))
        {
            result = sticks.moveStick;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(lookStickArea, myTouch.position))
        {
            result = sticks.lookStick;
        }
        

        /*
        this crap won't work!!!!
        if (movementStickArea.rect.Contains(myTouch.position))
        {
            result = sticks.moveStick;
        } else if (lookStickArea.rect.Contains(myTouch.position))
        {
            result = sticks.lookStick;
        }

        */

        

        return result;
    }



}
