using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public Global global;
    public CanvasScaler canvasScalar;

    public GameObject overlays;
    public GameObject actions;
    public GameObject decals;
    public GameObject mobileMoveJoystick;
    public GameObject mobileLookJoystick;

    void Start()
    {
        canvasScalar.referenceResolution = new Vector2(global.width, global.height);

        float padding = 16.0f;

        if (global.platform == false)
        {
            overlays.SetActive(true);

            actions.SetActive(true);
            actions.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.0f);
            actions.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.0f);
            actions.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.0f);
            actions.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0.0f, padding, 0.0f);

            decals.SetActive(true);
            decals.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 0.0f);
            decals.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 0.0f);
            decals.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 0.0f);
            decals.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(padding, padding, 0.0f);

            mobileMoveJoystick.SetActive(false);
            mobileLookJoystick.SetActive(false);
        }
        else
        {
            overlays.SetActive(true);

            actions.SetActive(true);
            actions.GetComponent<RectTransform>().anchorMin = new Vector2(1.0f, 0.0f);
            actions.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 0.0f);
            actions.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            float radius = mobileLookJoystick.GetComponent<RectTransform>().rect.width;
            radius = (2 * padding) + radius;
            actions.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-radius, radius, 0.0f);

            decals.SetActive(true);
            decals.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.0f);
            decals.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.0f);
            decals.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.0f);
            decals.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0.0f, padding, 0.0f);

            mobileMoveJoystick.SetActive(true);
            mobileLookJoystick.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
