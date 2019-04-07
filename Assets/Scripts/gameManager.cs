using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    private int npcCount;
    // Start is called before the first frame update
    void Start()
    {
        npcCount = GameObject.FindGameObjectsWithTag("NPC").Length;
    }
    
    //player kills an npc
    public void killNPC ()
    {
        npcCount -= 1;
        if (npcCount <= 0)
        {
            endGame();   
        }
    }

    //what happens when the player kills all npcs
    private void endGame()
    {
        SceneManager.LoadScene(0);
    }
}
