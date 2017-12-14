using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    private Transform target;
    private Vector3 offset;
    private int smoothing = 3;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position; //局部坐标
    }
	
	// Update is called once per frame
	void LateUpdate () {
        //把offset转换为target下的世界坐标
        Vector3 targetPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position , targetPosition, Time.deltaTime * smoothing);
        transform.LookAt(target.position);
	}
}
