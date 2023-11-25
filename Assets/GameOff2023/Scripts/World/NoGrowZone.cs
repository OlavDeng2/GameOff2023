using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NoGrowZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ScalableObject>() == null) return;
        other.gameObject.GetComponent<ScalableObject>().canGrow = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<ScalableObject>() == null) return;
        other.gameObject.GetComponent<ScalableObject>().canGrow = true;



    }
}
