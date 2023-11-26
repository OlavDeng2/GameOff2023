using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class ScalableObject : MonoBehaviour
{
    [HideInInspector]
    public bool canGrow = true;
    public enum Scale { Small, Medium, Big};

    [SerializeField]
    private Vector3 smallScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField]
    private Vector3 midScale = new Vector3(1.0f, 1.0f, 1.0f);
    [SerializeField]
    private Vector3 bigScale = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField]
    public Scale currentScale = Scale.Medium;

    [SerializeField]
    private float growthJump = 0.5f;

    [SerializeField]
    private AudioClip failToScaleAudio;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool Shrink()
    {
        
        switch (currentScale)
        {
            case Scale.Big:
                this.transform.localScale = midScale;
                currentScale = Scale.Medium;
                return true;
            case Scale.Medium:
                this.transform.localScale = smallScale;
                currentScale = Scale.Small;
                return true;
            default:
                //smallest scale, can't do anything
                audioSource.PlayOneShot(failToScaleAudio);
                return false;
        }
    }

    public virtual bool Grow()
    {
        //TODO: Give player some feedback about not being able to grow in size in this area
        //TODO: Return false to make sure that potion doesnt get used up if not able to grow
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (!canGrow)
        {
            audioSource.PlayOneShot(failToScaleAudio);
            return false;
        }

        switch (currentScale)
        {
            case Scale.Small:
                this.transform.localScale = midScale;
                currentScale = Scale.Medium;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + growthJump, this.transform.position.z);
                return true;
            case Scale.Medium:
                this.transform.localScale = bigScale;
                currentScale = Scale.Big;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + growthJump, this.transform.position.z);
                return true;

            default:
                //biggest scale, can't do anything
                audioSource.PlayOneShot(failToScaleAudio);
                return false;
        }
    }
}
