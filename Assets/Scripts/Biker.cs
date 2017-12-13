using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biker : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float v = Input.GetAxisRaw("Vertical");
        animator.SetInteger("Vertical", (int)v);
        transform.Translate(Vector3.forward * v * Time.deltaTime * 3);

	}
}
