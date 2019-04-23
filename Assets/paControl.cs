using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paControl : MonoBehaviour
{
    public AudioClip[] calls;
    public AudioSource musicSource;
    public AudioSource callSource;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(callOut());
    }

    IEnumerator callOut ()
    {
        while (true)
        {
            float timetowait = Random.Range(30f, 240f);
            yield return new WaitForSeconds(timetowait);
            AudioClip randomCall = calls[Random.Range(0, calls.Length)];
            musicSource.mute = true;
            callSource.clip = randomCall;
            callSource.Play();
            yield return new WaitForSeconds(randomCall.length);
            musicSource.mute = false;
        }
    }

    
}
