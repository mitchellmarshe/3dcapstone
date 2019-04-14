using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ontriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponentInParent<ReactiveAIMK2>().OnCloseTriggerEnter(other);
    }
}
