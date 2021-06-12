using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableBase : MonoBehaviour
{
    public void AttachItem(GameObject newItem)
    {
        AttachableSlot[] slots = transform.GetComponentsInChildren<AttachableSlot>();

        foreach(AttachableSlot slot in slots)
        {
            if (slot.CanAttach(newItem))
                slot.Attach(newItem);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
