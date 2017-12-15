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
    private int SliderId = Animator.StringToHash("Slide");
    private int IsHoldLogId = Animator.StringToHash("IsHoldLog");

    private Vector3 matchTarget = Vector3.zero;
    private CharacterController character;

    public GameObject unityLog;
    public Transform leftHand;
    public Transform rightHand;

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

        processVault();
        processSlider();

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

    private void processVault()
    {
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
    }

    private void processSlider()
    {
        bool isSlider = false;
        if (animator.GetFloat(SpeedZId) > 3 && animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 1.5f, transform.forward, out hit, 3))
            {
                if (hit.collider.tag == "Obstacle")
                {
                    if (hit.distance > 2)
                    {
                        Vector3 point = hit.point;
                        point.y = 0;
                        matchTarget = point + transform.forward * 2;
                        isSlider = true;
                    }
                }
            }
        }
        animator.SetBool(SliderId, isSlider);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Slide") && animator.IsInTransition(0) == false)
        {
            animator.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(1, 0, 1), 0), 0.17f, 0.67f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wood")
        {
            Destroy(other.gameObject);
            CarryWood();
        }
    }

    private void CarryWood()
    {
        unityLog.SetActive(true);
        animator.SetBool(IsHoldLogId, true);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (layerIndex == 1)
        {
            int wight = animator.GetBool(IsHoldLogId) ? 1 : 1;
            //当前层是hold log 层调用
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, wight);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, wight);
        }
    }

}
