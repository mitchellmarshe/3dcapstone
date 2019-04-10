using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class dragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragOnSurfaces = true;

    private GameObject tmpDecal;
    private Camera camComponent;

    private SpriteRenderer spriteRenderer;
    private Sprite currentSprite;

    private Color invalid;
    private Color valid;

    private bool validPlacement = false;
    private bool cooldown = false;

    public void Awake()
    {
        camComponent = GameObject.Find("Player").GetComponentInChildren<Camera>();
        tmpDecal = camComponent.GetComponentInParent<Controller2>().tmpDecal;
        spriteRenderer = tmpDecal.GetComponentInChildren<SpriteRenderer>();
        //tmpDecal.SetActive(false);
        
    }

    public void Start()
    {
        spriteRenderer.gameObject.SetActive(false);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (tmpDecal.GetComponent<decalManager2>().isDecalReady(gameObject.GetComponent<Image>()))
        {

            //tmpDecal.SetActive(true);
            //tmpDecal.GetComponent<decalManager>().disableDecalEffects();
            spriteRenderer.gameObject.SetActive(true);
            spriteRenderer.sprite = gameObject.GetComponent<Image>().sprite;
            invalid = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, .4f);
            valid = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            validPlacement = false;
            cooldown = false;

        } else
        {
            Debug.Log("Decal not ready to be placed");
            cooldown = true;
            spriteRenderer.gameObject.SetActive(false);
        }

    }

    public void OnDrag(PointerEventData data)
    {
        if (!cooldown)
        {
            validPlacement = false;
            RaycastHit rayHit;
            Ray ray = camComponent.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out rayHit, 12))
            {
                GameObject other = rayHit.collider.gameObject;
                Vector3 hitPoint = rayHit.point;
                Vector3 hitNormal = rayHit.normal;
                tmpDecal.transform.position = hitPoint;
                tmpDecal.transform.LookAt(hitPoint - hitNormal);
                if (other.tag == "Ignore")
                {
                    validPlacement = true;
                    spriteRenderer.color = valid;

                }
                else // the decal has hit something but it is invalid placement
                {
                    spriteRenderer.color = invalid;
                }
            }
            else // Ray has not hit anything at all, decal should float in free space as invalid placement
            {

                spriteRenderer.color = invalid;
                Vector3 origin = camComponent.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                Vector3 newPos = origin + (camComponent.transform.forward * 3);
                tmpDecal.transform.position = newPos;
                tmpDecal.transform.rotation = camComponent.gameObject.transform.rotation;
            }
        }
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        if (!cooldown)
        {
            //tmpDecal.SetActive(false);
            if (validPlacement)
            {
                //tmpDecal.GetComponent<decalManager>().enableDecalEffects();
                tmpDecal.GetComponent<decalManager2>().placedADecal(gameObject.GetComponent<Image>());
                GameObject newDecal = Instantiate(spriteRenderer.gameObject, spriteRenderer.transform.position, spriteRenderer.transform.rotation, null);
                newDecal.transform.localScale = spriteRenderer.transform.lossyScale;
                newDecal.tag = "Decal";
                Destroy(newDecal, 15.0f);
                
            }
            else
            {

                //tmpDecal.SetActive(false);
            }
            spriteRenderer.gameObject.SetActive(false);
        }
    }

    /*

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        m_DraggingIcon = new GameObject("icon");

        m_DraggingIcon.transform.SetParent(canvas.transform, false);
        m_DraggingIcon.transform.SetAsLastSibling();

        var image = m_DraggingIcon.AddComponent<Image>();

        image.sprite = GetComponent<Image>().sprite;
        //image.SetNativeSize();
        m_DraggingIcon.transform.localScale = new Vector3(m_DraggingIcon.transform.localScale.x * 3, m_DraggingIcon.transform.localScale.y * 3, m_DraggingIcon.transform.localScale.z * 3);

        if (dragOnSurfaces)
            m_DraggingPlane = transform as RectTransform;
        else
            m_DraggingPlane = canvas.transform as RectTransform;

        //SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        if (m_DraggingIcon != null)
        {
            //SetDraggedPosition(data);
            Ray ray = camComponent.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 4))
            {
                GameObject other = rayHit.collider.gameObject;
                if (other.tag == "Ignore")
                {
                    Vector3 hitPoint = rayHit.point;
                    Vector3 hitNormal = rayHit.normal;
                    m_DraggingIcon.transform.position = hitPoint;
                    m_DraggingIcon.transform.LookAt(hitPoint - hitNormal);
                }
            }
        }
            
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = m_DraggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_DraggingIcon != null)
        {
            Destroy(m_DraggingIcon);
        }
    }

    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }
    */
}
