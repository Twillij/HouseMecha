using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableSlot : MonoBehaviour
{
    private bool empty = true;

    public bool TryAttachItem(AttachableItem newItem)
    {
        if (CanAttachItem(newItem))
        {
            // find the inactive item in the hierarchy
            AttachableItem item = GetComponentInChildren<AttachableItem>(true);

            if (item != null)
            {
                item.gameObject.SetActive(true);
                empty = false;
                return true;
            }
        }

        return false;
    }

    private bool CanAttachItem(AttachableItem newItem)
    {
        if (empty && newItem.CompareTag(this.tag))
            return true;
        else
            return false;
    }
}
