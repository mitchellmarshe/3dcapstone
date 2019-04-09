using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public Global global;
    public CanvasScaler canvasScalar;

    public GameObject overlays;
    public GameObject menu;
    public GameObject knob;
    public GameObject actions;
    public GameObject decals;
    public GameObject mobileMoveJoystick;
    public GameObject mobileLookJoystick;

    private float padding;

    private void Awake()
    {
        padding = 16.0f;
    }

    private void Start()
    {
        canvasScalar.referenceResolution = new Vector2(global.width, global.height);

        if (global.platform == false)
        {
            menu.SetActive(true);
            SetMenu();
            SetKnob();

            mobileMoveJoystick.SetActive(false);
            mobileLookJoystick.SetActive(false);

            if (global.currentScene == global.startScene)
            {
                actions.SetActive(false);
                decals.SetActive(false);
            }
            else
            {
                actions.SetActive(true);
                SetActions();

                decals.SetActive(true);
                SetDecals();
            }
        }
        else
        {
            menu.SetActive(true);
            SetMenu();
            SetKnob();

            mobileLookJoystick.SetActive(true);
            SetMobileLookJoystick();

            if (global.currentScene == global.startScene)
            {
                actions.SetActive(false);
                decals.SetActive(false);
                mobileMoveJoystick.SetActive(false);
            }
            else
            {
                actions.SetActive(true);
                SetActions();

                decals.SetActive(true);
                SetDecals();

                mobileMoveJoystick.SetActive(true);
                SetMobileMoveJoystick();
            }
        }
    }

    private void Update()
    {
        
    }

    public void ShowOverlays(bool trigger)
    {
        overlays.transform.GetChild(0).gameObject.SetActive(trigger);
        overlays.transform.GetChild(1).gameObject.SetActive(trigger);
        overlays.SetActive(trigger);
    }

    public void SetOverlays()
    {

    }

    private void SetMenu()
    {

    }

    private void SetKnob()
    {
        knob.GetComponent<RectTransform>().anchoredPosition3D = 
            new Vector3((-global.width / 2) + padding, (global.height / 2) - padding, 0.0f);
    }

    private void SetActions()
    {
        if (global.platform == false)
        {
            actions.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.0f);
            actions.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.0f);
            actions.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.0f);
            float height = decals.GetComponent<RectTransform>().rect.height;
            height = (2 * padding) + height;
            actions.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0.0f, height, 0.0f);
        }
        else
        {
            actions.GetComponent<RectTransform>().anchorMin = new Vector2(1.0f, 0.0f);
            actions.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 0.0f);
            actions.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            float radius = mobileLookJoystick.GetComponent<RectTransform>().rect.width;
            radius = (2 * padding) + radius;
            actions.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-radius, radius, 0.0f);
        }
    }

    private void SetDecals()
    {

    }

    private void SetMobileMoveJoystick()
    {

    }

    private void SetMobileLookJoystick()
    {

    }
}
