using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHypnoOnDestroy : MonoBehaviour
{
    public List<ReactiveAIMK2> hypnoNPCS = new List<ReactiveAIMK2>();
    // Start is called before the first frame update
    private void OnDestroy()
    {
        foreach(ReactiveAIMK2 npc in hypnoNPCS)
        {
            
            npc.endHypnotized();
            Debug.Log("Called End Hypno");
        }
    }
}
