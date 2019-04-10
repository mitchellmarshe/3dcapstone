using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] tips;
    private int i = 0;

    public void nextTip()
    {
        if(i >= tips.Length)
        {
            gameObject.SetActive(false);
        } else
        {
            i++;
            tips[i - 1].SetActive(false);
            tips[i].SetActive(true);
        }
        
    }
}
