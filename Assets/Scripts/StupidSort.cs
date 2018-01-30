using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidSort : P_AbstractSort {

    void Update() {

        if (Input.GetKeyDown(KeyCode.Y) && !coroutineWorked) {
            Debug.Log("Y pressed ");

            changeBallsPlace(arrayBalls[1].transform,arrayBalls[2].transform);

        }
        if (Input.GetKeyDown(KeyCode.R)) {
            resetBallArray();
        }

    }

    /*
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
     */


}
