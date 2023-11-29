using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectToCheck;
    [SerializeField]
    private UnityEvent OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == gameObjectToCheck )
        {
            if(other.gameObject.GetComponent<GrababbleItem>()!= null)
            {
                if(other.gameObject.GetComponent<GrababbleItem>().isHeld)
                {
                    return;
                }
            }
            OnTrigger?.Invoke();
        }
    }

}
