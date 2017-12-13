using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Animator animator;

    private int SpeedId = Animator.StringToHash("Speed");
    private int IsSpeedUpId = Animator.StringToHash("IsSpeedUp");
    private int HorizontalId = Animator.StringToHash("Horizontal");

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat(SpeedId, Input.GetAxis("Vertical"));
        animator.SetFloat(HorizontalId, Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool(IsSpeedUpId, true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool(IsSpeedUpId, false);
        }
	}
}
