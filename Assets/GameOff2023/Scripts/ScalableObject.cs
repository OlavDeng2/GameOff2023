using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScallableObject : MonoBehaviour
{
    [SerializeField]
    private float defaultScale = 1.0f;
    [SerializeField]
    private float shrunkScale = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shrink()
    {
        this.transform.localScale = new Vector3(shrunkScale, shrunkScale, shrunkScale);
    }

    public void Grow()
    {
        this.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);

    }
}
