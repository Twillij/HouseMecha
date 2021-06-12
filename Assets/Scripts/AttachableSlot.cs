using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableSlot : MonoBehaviour
{
    private bool empty = true;

    public bool CanAttach(GameObject newItem)
    {
        if (empty && newItem.CompareTag(this.tag))
            return true;
        else
            return false;
    }

    public void Attach(GameObject newItem)
    {
        newItem.transform.parent = this.transform;
        newItem.GetComponent<Rigidbody>().isKinematic = true;
    }
}
