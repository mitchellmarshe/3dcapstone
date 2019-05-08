using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionHandler : MonoBehaviour
{
    // Kill # Npcs with a... decal, thrown obj, interactable obj
    enum TaskType { decal, interation, thrown};

    string basicTxt = " NPCs with a ";
    //decal detection would exist in both the decal system in dragHandler.cs and in decalManager2.cs
    //thrown obj detection and interactable could be hooked into reactiveMK2AI.cs
    public int task1Type;
    public int task2Type;
    public int task3Type;
    public Text task1Txt;
    public Text task2Txt;
    public Text task3Txt;
    public int task1Num;
    public int task2Num;
    public int task3Num;
    public string task1Name;
    public string task2Name;
    public string task3Name;

    public string[] decals;
    public string[] interactionObjs;
    public string[] throwables;

    


    // Start is called before the first frame update
    void Start()
    {
        pickQuests();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void pickQuests()
    {
        task1Type = Random.Range(1, 4);
        task2Type = Random.Range(1, 4);
        task3Type = Random.Range(1, 4);
        task1Num = Random.Range(1, 4);
        task2Num = Random.Range(2, 8);
        task3Num = Random.Range(1, 5);
        
        if (task1Type == 1)
        {
            task1Name = decals[Random.Range(0, decals.Length)];
            task1Txt.text = "Kill " + task1Num + basicTxt + task1Name + " decal";
        } else if(task1Type == 2)
        {
            task1Name = interactionObjs[Random.Range(0, interactionObjs.Length)];
            task1Num = 1;
            task1Txt.text = "Interact with " + task1Name.Substring(0, task1Name.Length - 7);
        } else
        {
            task1Name = throwables[Random.Range(0, throwables.Length)];
            task1Txt.text = "Throw " + task1Num + ": " + task1Name;
        }

        if (task2Type == 1)
        {
            task2Name = decals[Random.Range(0, decals.Length)];
            task2Txt.text = "Kill " + task2Num + basicTxt + task2Name + " decal";
        }
        else if (task2Type == 2)
        {
            task2Name = interactionObjs[Random.Range(0, interactionObjs.Length)];
            task2Num = 1;
            task2Txt.text = "Interact with " + task2Name.Substring(0, task2Name.Length - 7);
        }
        else
        {
            task2Name = throwables[Random.Range(0, throwables.Length)];
            task2Txt.text = "Throw " + task2Num + ": " + task2Name;
        }

        if (task3Type == 1)
        {
            task3Name = decals[Random.Range(0, decals.Length)];
            task3Txt.text = "Kill " + task3Num + basicTxt + task3Name + " decal";
        }
        else if (task3Type == 2)
        {
            task3Name = interactionObjs[Random.Range(0, interactionObjs.Length)];
            task3Num = 1;
            
            task3Txt.text = "Interact with " + task3Name.Substring(0,task3Name.Length-7);
        }
        else
        {
            task3Name = throwables[Random.Range(0, throwables.Length)];
            task3Txt.text = "Throw " + task3Num + ": " + task3Name;
        }


    }

    public bool checkDecal(string other)
    {
        if(task1Type == 1 && other == task1Name)
        {
            return true;
        }

        if (task2Type == 1 && other == task2Name)
        {
            return true;
        }

        if (task3Type == 1 && other == task3Name)
        {
            return true;
        }
        return false;
    }

    public void killed4Task(int num)
    {
        if(num == 1)
        {
            task1Num--;
            if(task1Num < 1)
            {
                completeTask(1);
            }
        } else if(num == 2)
        {
            task2Num--;
            if (task2Num < 1)
            {
                completeTask(2);
            }
        } else
        {
            task3Num--;
            if (task3Num < 1)
            {
                completeTask(3);
            }
        }
    }

    public void completeTask(int num)
    {
        if(num == 1)
        {
            task1Txt.color = Color.green;
        } else if(num == 2)
        {
            task2Txt.color = Color.green;
        } else
        {
            task3Txt.color = Color.green;
        }
    }
    


}
