using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    [SerializeField]
    Transform pointA;
    [SerializeField]
    Transform pointB;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A)) {
            StartCoroutine(moveToTarget(pointB.position,pointA.position,10f));
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            StartCoroutine(moveToTarget(pointA.position, pointB.position, 10f));
        }

	}

    IEnumerator moveToTarget(Vector3 source, Vector3 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        transform.position = target;
    }
}
