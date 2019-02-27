using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBobbing : MonoBehaviour
{
    Transform myTransform;
    float rand;
    bool adding;
    float counter;
    float timeDelta;
    // Start is called before the first frame update
    void Start()
    {
        timeDelta = 0;
        counter = 0;
        myTransform = gameObject.transform;
        adding = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeDelta += Time.deltaTime;
        if (timeDelta > .1)
        {
            timeDelta = 0;
            rand = Random.Range(-.04f, .04f);

            if (adding)
            {
                counter += 0.1f;
                myTransform.position = new Vector3(myTransform.position.x + rand, myTransform.position.y + 0.1f, myTransform.position.z - rand);
                myTransform.rotation = new Quaternion(myTransform.rotation.x + rand, myTransform.rotation.y + rand, myTransform.position.z - rand, myTransform.rotation.w);
                if (counter >= 2)
                {
                    adding = false;
                    counter = 0;
                }
            }
            else
            {
                counter += 0.1f;
                myTransform.position = new Vector3(myTransform.position.x - rand, myTransform.position.y - 0.1f, myTransform.position.z + rand);
                myTransform.rotation = new Quaternion(myTransform.rotation.x - rand, myTransform.rotation.y - rand, myTransform.position.z + rand, myTransform.rotation.w);
                if (counter >= 2)
                {
                    adding = true;
                    counter = 0;
                }
            }
        }
        
    }
}
