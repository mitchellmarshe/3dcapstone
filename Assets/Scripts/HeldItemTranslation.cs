using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItemTranslation : MonoBehaviour
{
    private bool hitConstraint = true;
    private float counter = 0;
    public bool holding;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (holding)
        {
            transform.Rotate(2, 1, 0);
            counter += Time.fixedDeltaTime;
            if (counter >= 2.5f)
            {
                hitConstraint = !hitConstraint;
                counter = 0;
            }
            if (hitConstraint)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + .005f, transform.localPosition.z);
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - .005f, transform.localPosition.z);
            }
        }
    }

    public void resetAnim(bool hold)
    {
        transform.localPosition = new Vector3(0, 0, 0);
        counter = 0;
        hitConstraint = true;
        holding = hold;
    }
}
