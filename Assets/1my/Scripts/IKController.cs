using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    [SerializeField] Transform Head;
    [SerializeField] Transform Left;
    [SerializeField] Transform Right;
    [SerializeField] Transform Target;

    [SerializeField] bool isIkActive;
    [SerializeField] bool isGroup;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator != null)
        {
            if (isIkActive)
            {
                if (!Head)
                {
                    return;
                }
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(Target.position);

                Head.LookAt(Target);

                if (!Left)
                {
                    return;
                }
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, Left.position);

                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKPosition(AvatarIKGoal.RightHand, Right.position);
            }

            if (isGroup)
            {
                Left.SetParent(Head);
                Right.SetParent(Head);
            }
        }

    }
}
