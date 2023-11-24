using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  IGrabbableItem 
{
    void Grab(Transform newParent);
    void Drop();
    void Throw();

}
