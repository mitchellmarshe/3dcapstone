using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script to control GUI.

public class GUI : MonoBehaviour
{
    [Header("Scripts")]
    public Global global;
    public CanvasScaler canvasScalar;

    [Header("GUI")]
    public GameObject overlays;
    public GameObject menu;
    public GameObject knob;
    public GameObject actions;
    public GameObject actionsLeft;
    public GameObject actionsRight;
    public GameObject hotkey1;
    public GameObject hotkey2;
    public GameObject hotkey3;
    public GameObject hotkey4;
    public GameObject decals;
    public GameObject mobileMoveJoystick;
    public GameObject mobileLookJoystick;

    [Header("Tutorials")]
    public GameObject tutorial;

    public GameObject lookTutorial;
    public bool lookTutorialOn;

    public GameObject clickTutorial;
    public bool clickTutorialOn;

    public GameObject moveTutorial;
    public bool moveTutorialOn;

    [Header("Options")]
    public Toggle tutorialToggle;
    public Slider guiSlider;
    public Slider lookSlider;
    public Slider moveSlider;

    private float padding;
    private float guiScale;
    private float guiScale2;
    private float width;
    private float height;

    private void Awake()
    {
        padding = 16.0f;
        width = global.width;
        height = global.height;
        Resolution();
        guiScale = guiScale2;
        guiSlider.value = guiScale;

        lookTutorialOn = false;
        clickTutorialOn = false;
        moveTutorialOn = false;
    }

    private void Start()
    {
        canvasScalar.referenceResolution = new Vector2(global.width, global.height);
        Resolution();

        TutorialToggle();
        GUISlider();

        SetOverlays();
        SetTutorial();
        SetLookTutorial();
        SetClickTutorial();
        SetMoveTutorial();

        menu.SetActive(true);
        SetMenu();
        SetKnob();

        if (global.platform == false) // PC
        {
            ShowDecals(false);

            mobileMoveJoystick.SetActive(false);
            mobileLookJoystick.SetActive(false);

            hotkey1.SetActive(true);
            hotkey2.SetActive(true);
            hotkey3.SetActive(true);
            hotkey4.SetActive(true);

            if (global.currentScene == global.startScene)
            {
                ShowActions(false);

                ShowLookTutorial();
                ShowLookTutorial();
                ShowMoveTutorial();
            }
            else
            {
                ShowActions(true);

                ShowLookTutorial();
                ShowClickTutorial();
                ShowMoveTutorial();
            }
        }
        else // Mobile
        {
            mobileLookJoystick.SetActive(true);
            SetMobileLookJoystick();

            hotkey1.SetActive(false);
            hotkey2.SetActive(false);
            hotkey3.SetActive(false);
            hotkey4.SetActive(false);

            ShowLookTutorial();

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
        canvasScalar.referenceResolution = new Vector2(global.width, global.height);

        TutorialToggle();

        if (guiSlider.value != guiScale)
        {
            GUISlider();
            SetDecals();
            SetActions();
            SetKnob();

            if (global.platform == true)
            {
                SetMobileMoveJoystick();
                SetMobileLookJoystick();
            }
        }

        if (width != global.width || height != global.height)
        {
            Resolution();
            SetMenu();

            width = global.width;
            height = global.height;
        }
    }

    public void Resolution()
    {
        guiScale2 = 1.0f;

        if (global.width <= 1200 && global.height <= 1300)
        {
            guiScale2 = 0.5f;
        }

        if (global.width <= 600 && global.height <= 900)
        {
            guiScale2 = 0.25f;
        }
    }

    public void TutorialToggle()
    {
        if (global.tutorial == false)
        {
            tutorialToggle.interactable = false;
            tutorialToggle.isOn = false;
            return;
        }

        global.tutorial = tutorialToggle.isOn;
    }

    public void GUISlider()
    {
        guiScale = guiSlider.value;
    }

    public void ShowOverlays(bool trigger)
    {
        overlays.transform.GetChild(0).gameObject.SetActive(trigger);
        overlays.transform.GetChild(1).gameObject.SetActive(trigger);
        overlays.SetActive(trigger);
    }

    public void ShowTutorial(bool trigger)
    {
        tutorial.SetActive(trigger);
    }

    public void ShowLookTutorial()
    {
        lookTutorial.SetActive(lookTutorialOn);
        lookTutorialOn = !lookTutorialOn;
    }

    public void ShowClickTutorial()
    {
        clickTutorial.SetActive(clickTutorialOn);
        clickTutorialOn = !clickTutorialOn;
    }

    public void ShowMoveTutorial()
    {
        moveTutorial.SetActive(moveTutorialOn);
        moveTutorialOn = !moveTutorialOn;
    }

    public void ShowActions(bool trigger)
    {
        actions.SetActive(trigger);

        if (trigger == true)
        {
            SetActions();
        }
    }

    public void ShowDecals(bool trigger)
    {
        decals.SetActive(trigger);

        if (trigger == true)
        {
            SetDecals();
        }
    }

    public void SetOverlays()
    {
        overlays.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        overlays.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        overlays.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        overlays.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0.0f, 0.0f, 0.0f);

        overlays.transform.GetChild(0).GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        overlays.transform.GetChild(0).GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        overlays.transform.GetChild(0).GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        overlays.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0.0f, 0.0f, 0.0f);
        overlays.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(global.width, global.height);
        overlays.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

        overlays.transform.GetChild(1).GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        overlays.transform.GetChild(1).GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        overlays.transform.GetChild(1).GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        overlays.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0.0f, 0.0f, 0.0f);
        overlays.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(1024.0f, 512.0f);
        overlays.transform.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(guiScale2, guiScale2, guiScale2);
    }

    private void SetMenu()
    {
        menu.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        menu.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        menu.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        menu.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0.0f, 0.0f, 0.0f);
        menu.GetComponent<RectTransform>().localScale = new Vector3(guiScale2, guiScale2, guiScale2);
    }

    private void SetKnob()
    {
        if (global.platform == false)
        {
            knob.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
            knob.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
            knob.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
            knob.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(padding, -padding, 0.0f);
            knob.GetComponent<RectTransform>().localScale = new Vector3(guiScale, guiScale, guiScale);
        }
        else
        {
            knob.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
            knob.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
            knob.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
            knob.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(padding, -padding, 0.0f);
            knob.GetComponent<RectTransform>().localScale = new Vector3(guiScale * 1.5f, guiScale * 1.5f, guiScale * 1.5f);
        }
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
            actions.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0.0f, height * guiScale, 0.0f);
            actions.GetComponent<RectTransform>().localScale = new Vector3(guiScale, guiScale, guiScale);

            actionsLeft.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-128.0f, 0.0f, 0.0f);
            actionsLeft.GetComponent<RectTransform>().eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            actionsLeft.transform.GetChild(0).GetComponent<RectTransform>().eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            actionsLeft.transform.GetChild(1).GetComponent<RectTransform>().eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

            actionsRight.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(128.0f, 0.0f, 0.0f);
        }
        else
        {
            actions.GetComponent<RectTransform>().anchorMin = new Vector2(1.0f, 0.0f);
            actions.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 0.0f);
            actions.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            float radius = mobileLookJoystick.GetComponent<RectTransform>().rect.width;
            radius = (4 * padding) + radius;
            actions.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-radius * guiScale, radius * guiScale, 0.0f);
            actions.GetComponent<RectTransform>().localScale = new Vector3(guiScale, guiScale, guiScale);

            actionsLeft.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0.0f, -((radius / 2) + (padding / 2)), 0.0f);
            actionsLeft.GetComponent<RectTransform>().eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
            actionsLeft.GetComponent<RectTransform>().localScale = new Vector3(guiScale * 1.5f, guiScale * 1.5f, guiScale * 1.5f);
            actionsLeft.transform.GetChild(0).GetComponent<RectTransform>().eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
            actionsLeft.transform.GetChild(1).GetComponent<RectTransform>().eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);

            actionsRight.GetComponent<RectTransform>().anchoredPosition3D = new Vector3((radius / 2) + (padding / 2), 0.0f, 0.0f);
            actionsRight.GetComponent<RectTransform>().localScale = new Vector3(guiScale * 1.5f, guiScale * 1.5f, guiScale * 1.5f);
        }
    }

    private void SetDecals()
    {
        decals.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.0f);
        decals.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.0f);
        decals.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.0f);
        decals.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0.0f, padding, 0.0f);
        decals.GetComponent<RectTransform>().localScale = new Vector3(guiScale, guiScale, guiScale);
    }

    private void SetMobileMoveJoystick()
    {
        mobileMoveJoystick.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 0.0f);
        mobileMoveJoystick.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 0.0f);
        mobileMoveJoystick.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 0.0f);
        mobileMoveJoystick.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(padding, padding, 0.0f);
        mobileMoveJoystick.GetComponent<RectTransform>().localScale = new Vector3(guiScale, guiScale, guiScale);
    }

    private void SetMobileLookJoystick()
    {
        mobileLookJoystick.GetComponent<RectTransform>().anchorMin = new Vector2(1.0f, 0.0f);
        mobileLookJoystick.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 0.0f);
        mobileLookJoystick.GetComponent<RectTransform>().pivot = new Vector2(1.0f, 0.0f);
        mobileLookJoystick.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-padding, padding, 0.0f);
        mobileLookJoystick.GetComponent<RectTransform>().localScale = new Vector3(guiScale, guiScale, guiScale);
    }

    private void SetTutorial()
    {
        tutorial.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        tutorial.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        tutorial.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        tutorial.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0.0f, 0.0f, 0.0f);
        tutorial.GetComponent<RectTransform>().localScale = new Vector3(guiScale2, guiScale2, guiScale2);
    }

    private void SetLookTutorial()
    {
        lookTutorial.GetComponent<RectTransform>().anchorMin = new Vector2(1.0f, 0.0f);
        lookTutorial.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 0.0f);
        lookTutorial.GetComponent<RectTransform>().pivot = new Vector2(1.0f, 0.0f);
        lookTutorial.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-padding, padding, 0.0f);
        lookTutorial.GetComponent<RectTransform>().localScale = new Vector3(guiScale2, guiScale2, guiScale2);
    }

    private void SetClickTutorial()
    {
        clickTutorial.GetComponent<RectTransform>().anchorMin = new Vector2(1.0f, 0.0f);
        clickTutorial.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 0.0f);
        clickTutorial.GetComponent<RectTransform>().pivot = new Vector2(1.0f, 0.0f);
        clickTutorial.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-padding, padding, 0.0f);
        clickTutorial.GetComponent<RectTransform>().localScale = new Vector3(guiScale2, guiScale2, guiScale2);
    }

    private void SetMoveTutorial()
    {
        moveTutorial.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 0.0f);
        moveTutorial.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 0.0f);
        moveTutorial.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 0.0f);
        moveTutorial.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(padding, padding, 0.0f);
        moveTutorial.GetComponent<RectTransform>().localScale = new Vector3(guiScale2, guiScale2, guiScale2);
    }
}
