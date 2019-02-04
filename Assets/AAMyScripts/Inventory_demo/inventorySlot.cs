using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class inventorySlot : MonoBehaviour, IDropHandler
{
    public GameObject item
    {
        get
        {
            if(transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            dragHandler.itemBeingDragged.transform.SetParent(transform);
        } else
        {
            Transform tmpTrans = transform;
            Vector3 tmpPos = transform.position;
            //item.transform.SetParent(dragHandler.itemBeingDragged.transform.parent);
            //item.transform.position = dragHandler.itemBeingDragged.transform.position;
            dragHandler.itemBeingDragged.transform.SetParent(tmpTrans);
            //dragHandler.itemBeingDragged.transform.position = tmpPos;

        }
    }
}
