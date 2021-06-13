using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableItem : MonoBehaviour
{
    public Vector3 scaleValue = Vector3.zero;

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
