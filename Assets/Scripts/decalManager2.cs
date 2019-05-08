using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class decalManager2 : MonoBehaviour
{
    public Global global;
    public Guardi guardi;

    public Image redrumSprite;
    public Image unicornSprite;
    public Image skullSprite;
    public Image lizardSprite;
    public Image pentagramSprite;

    public float[] counters = new float[5] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    private float cooldownRedrum = 20f;
    private float cooldownUnicorn = 1f;
    private float cooldownSkull = 25f;
    private float cooldownLizard = 20f;
    private float cooldownPentagram = 90f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tickTimers();
    }

    private void tickTimers()
    {
        if (counters[0] > 0)
        {
            counters[0] -= Time.deltaTime;
            redrumSprite.color = new Color(redrumSprite.color.r, redrumSprite.color.g, redrumSprite.color.b, 0.35f);
            //redrumSprite.color = new Color(redrumSprite.color.r, redrumSprite.color.g, redrumSprite.color.b, Mathf.Max(cooldownRedrum-counters[0] * 3, 0));
        } else
        {
            redrumSprite.color = new Color(redrumSprite.color.r, redrumSprite.color.g, redrumSprite.color.b, 1);
        }
        if (counters[1] > 0)
        {
            counters[1] -= Time.deltaTime;
            unicornSprite.color = new Color(unicornSprite.color.r, unicornSprite.color.g, unicornSprite.color.b, 0.35f);
            //unicornSprite.color = new Color(unicornSprite.color.r, unicornSprite.color.g, unicornSprite.color.b, Mathf.Max(cooldownUnicorn - counters[1] * 3, 0));
        } else
        {
            unicornSprite.color = new Color(unicornSprite.color.r, unicornSprite.color.g, unicornSprite.color.b, 1);
        }
        if (counters[2] > 0)
        {
            counters[2] -= Time.deltaTime;
            skullSprite.color = new Color(skullSprite.color.r, skullSprite.color.g, skullSprite.color.b, 0.35f);
            //skullSprite.color = new Color(skullSprite.color.r, skullSprite.color.g, skullSprite.color.b, Mathf.Max(cooldownSkull - counters[2] * 3, 0));
        } else
        {
            skullSprite.color = new Color(skullSprite.color.r, skullSprite.color.g, skullSprite.color.b, 1);
        }
        if (counters[3] > 0)
        {
            counters[3] -= Time.deltaTime;
            lizardSprite.color = new Color(lizardSprite.color.r, lizardSprite.color.g, lizardSprite.color.b, 0.35f);
            //lizardSprite.color = new Color(lizardSprite.color.r, lizardSprite.color.g, lizardSprite.color.b, Mathf.Max(cooldownLizard - counters[3] * 3, 0));
        } else
        {
            lizardSprite.color = new Color(lizardSprite.color.r, lizardSprite.color.g, lizardSprite.color.b, 1);
        }
        if (counters[4] > 0)
        {
            counters[4] -= Time.deltaTime;
            pentagramSprite.color = new Color(pentagramSprite.color.r, pentagramSprite.color.g, pentagramSprite.color.b, 0.35f);
            //pentagramSprite.color = new Color(pentagramSprite.color.r, pentagramSprite.color.g, pentagramSprite.color.b, Mathf.Max(cooldownPentagram - counters[4]*3, 0));
        } else
        {
            pentagramSprite.color = new Color(pentagramSprite.color.r, pentagramSprite.color.g, pentagramSprite.color.b, 1);
        }
    }

    public bool isDecalReady(Image other)
    {
        if(other.sprite == redrumSprite.sprite)
        {
            if(counters[0] <= 0)
            {
                return true;
            }
            return false;
        } else if (other.sprite == unicornSprite.sprite)
        {
            if (counters[1] <= 0)
            {
                return true;
            }
            return false;
        }
        else if (other.sprite == skullSprite.sprite)
        {
            if (counters[2] <= 0)
            {
                return true;
            }
            return false;
        }
        else if (other.sprite == lizardSprite.sprite)
        {
            if (counters[3] <= 0)
            {
                return true;
            }
            return false;
        }
        else if (other.sprite == pentagramSprite.sprite)
        {
            if (counters[4] <= 0)
            {
                return true;
            }
            return false;
        }
        else
        {
            Debug.Log("Image 'other' in isDecalReady does not have a decal sprite!!!");
        }
        return false;
    } // Returns true if the sprite other matches one of the decals and the counter is <= 0

    public void placedADecal(Image other)
    {
        // Guardi
        if (global.currentScene == global.mainScene && guardi.decal == false)
        {
            guardi.decal = true;
        }


        if (other.sprite == redrumSprite.sprite)
        {
            counters[0] = cooldownRedrum;
        }
        else if (other.sprite == unicornSprite.sprite)
        {
            counters[1] = cooldownUnicorn;
        }
        else if (other.sprite == skullSprite.sprite)
        {
            counters[2] = cooldownSkull;
        }
        else if (other.sprite == lizardSprite.sprite)
        {
            counters[3] = cooldownLizard;
        }
        else if (other.sprite == pentagramSprite.sprite)
        {
            counters[4] = cooldownPentagram;
        }
        else
        {
            Debug.Log("Image 'other' in isDecalReady does not have a decal sprite!!!");
        }

        
    }

}
