using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class ScalablePlayer : ScalableObject
{
    [SerializeField]
    private float smallJumpHeight = 0.2f;
    [SerializeField]
    private float mediumJumpHeight = 0.6f;
    [SerializeField]
    private float bigJumpHeight = 1.0f;

    private FirstPersonController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Grow()
    {
        base.Grow();
        SetCurrrentJump();
    }

    public override void Shrink()
    {
        base.Shrink();
        SetCurrrentJump();
    }

    private void SetCurrrentJump()
    {
        switch (currentScale)
        {
            case Scale.Small:
                playerController.JumpHeight = smallJumpHeight;
                break;
            case Scale.Medium:
                playerController.JumpHeight = mediumJumpHeight;
                break;
            case Scale.Big:
                playerController.JumpHeight = bigJumpHeight;
                break;
        }
    }
}
