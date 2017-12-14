using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Animator animator;

    private int SpeedId = Animator.StringToHash("Speed");
    private int IsSpeedUpId = Animator.StringToHash("IsSpeedUp");
    private int HorizontalId = Animator.StringToHash("Horizontal");
    private int SpeedRotateId = Animator.StringToHash("SpeedRotate");
    private int SpeedZId = Animator.StringToHash("SpeedZ");
    private int VaultId = Animator.StringToHash("Vault");
    private int ColliderId = Animator.StringToHash("Collider");

    private Vector3 matchTarget = Vector3.zero;
    private CharacterController character;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        animator.SetFloat(SpeedZId, Input.GetAxis("Vertical") * 4.1f);
        animator.SetFloat(SpeedRotateId, Input.GetAxis("Horizontal") * 126f);

        bool isVault = false;
        if (animator.GetFloat(SpeedZId) > 3 && animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.3f, transform.forward, out hit, 4))
            {
                if (hit.collider.tag == "Obstacle")
                {
                    if (hit.distance > 3)
                    {
                        Vector3 point = hit.point;
                        point.y = hit.collider.transform.position.y + hit.collider.bounds.size.y + 0.07f;
                        matchTarget = point;
                        isVault = true;
                    }
                }
            }
        }
        animator.SetBool(VaultId, isVault);
            
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Vault") && animator.IsInTransition(0) == false)
        {
            animator.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one, 0), 0.32f, 0.4f);
        }

        character.enabled = animator.GetFloat(ColliderId) < 0.5f;

        //animator.SetFloat(SpeedId, Input.GetAxis("Vertical") * 4.1f);
        //animator.SetFloat(SpeedId, Input.GetAxis("Vertical"));
        //animator.SetFloat(HorizontalId, Input.GetAxis("Horizontal"));

        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    animator.SetBool(IsSpeedUpId, true);
        //} 
        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    animator.SetBool(IsSpeedUpId, false);
        //}
    }
}
