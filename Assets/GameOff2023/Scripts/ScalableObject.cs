using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScalableObject : MonoBehaviour
{
    public enum Scale { Small, Medium, Big};

    [SerializeField]
    private Vector3 smallScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField]
    private Vector3 midScale = new Vector3(1.0f, 1.0f, 1.0f);
    [SerializeField]
    private Vector3 bigScale = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField]
    public Scale currentScale = Scale.Medium;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Shrink()
    {
        switch (currentScale)
        {
            case Scale.Big:
                this.transform.localScale = midScale;
                currentScale = Scale.Medium;
                break;
            case Scale.Medium:
                this.transform.localScale = smallScale;
                currentScale = Scale.Small;
                break;
            case Scale.Small:
                //smallest scale, can't do anything
                break;
        }
    }

    public virtual void Grow()
    {
        switch (currentScale)
        {
            case Scale.Small:
                this.transform.localScale = midScale;
                currentScale = Scale.Medium;
                break;
            case Scale.Medium:
                this.transform.localScale = bigScale;
                currentScale = Scale.Big;
                break;

            case Scale.Big:
                //biggest scale, can't do anything
                break;
        }
    }
}
