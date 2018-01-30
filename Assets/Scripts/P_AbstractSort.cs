using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class P_AbstractSort : MonoBehaviour {

    public Transform arrayPlacesParent;
    public Transform ballsParent;

    [HideInInspector] public List<GameObject> arrayPlaces = new List<GameObject>();
    [HideInInspector] public List<GameObject> arrayBalls = new List<GameObject>();
    [HideInInspector] public System.Random _random = new System.Random();

    [HideInInspector] public int[] tmpIntArr;

    public Transform centerObject;

    public bool coroutineWorked = false;

    public virtual void Start() {
        centerObject = Instantiate(centerObject);

        for (int i = 0; i < ballsParent.childCount; i++)
        {
            arrayBalls.Add(ballsParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < arrayPlacesParent.childCount; i++)
        {
            arrayPlaces.Add(arrayPlacesParent.GetChild(i).gameObject);
        }

        tmpIntArr = new int[arrayPlaces.Count];

        for (int i = 0; i < arrayPlaces.Count; i++)
        {
            tmpIntArr[i] = i;
            //Debug.Log("TmpIntArray: index " + i + ", value "+tmpIntArr[i]);
        }

        resetBallArray();
    }

    public void resetBallArray()
    {
        tmpIntArr = Shuffle(tmpIntArr);

        for (int i = 0; i < arrayPlaces.Count; i++)
        {
            //Debug.Log("Shuffled TmpIntArray: index " + i + ", value " + tmpIntArr[i]);
            arrayBalls[tmpIntArr[i]].transform.position = arrayPlaces[i].transform.position;
        }
    }

    int[] Shuffle(int[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(1, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }



        return array;
    }

    public void changeBallsPlace(Transform ballOne, Transform ballTwo)
    {


        Vector3 center = Vector3.Lerp(ballOne.position, ballTwo.position, 0.5f);

        centerObject.position = center;
        ballOne.SetParent(centerObject);
        ballTwo.SetParent(centerObject);

        /*centerObject.Rotate(Vector3.forward, 180);

        ballOne.Rotate(Vector3.forward, 180);
        ballTwo.Rotate(Vector3.forward, 180);*/

        float speed = 2f;
        //centerObject.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,0,180), Time.deltaTime*speed);
        StartCoroutine(rotateAroundCenter(centerObject, ballOne, ballTwo, speed));

        //ballOne.transform.RotateAround(center, 180);
        //ballTwo.transform.RotateAround(center, 180);

    }

    public IEnumerator rotateAroundCenter(Transform balls, Transform ballOne, Transform ballTwo, float overTime)
    {

        coroutineWorked = true;
        float startTime = Time.time;

        Vector3 rotBalls = balls.rotation.eulerAngles;
        rotBalls = new Vector3(0, 0, rotBalls.z + 180);

        Vector3 rotOne = ballOne.localRotation.eulerAngles;
        rotOne = new Vector3(0, 0, rotOne.z - 180);

        Vector3 rotTwo = ballTwo.localRotation.eulerAngles;
        rotTwo = new Vector3(0, 0, rotTwo.z - 180);

        while (Time.time < startTime + overTime)
        {
            balls.rotation = Quaternion.Slerp(balls.rotation, Quaternion.Euler(rotBalls), (Time.time - startTime) / overTime);

            ballOne.localRotation = Quaternion.Slerp(ballOne.localRotation, Quaternion.Euler(rotOne), (Time.time - startTime) / overTime);

            ballTwo.localRotation = Quaternion.Slerp(ballTwo.localRotation, Quaternion.Euler(rotTwo), (Time.time - startTime) / overTime);
            yield return null;
        }

        balls.rotation = Quaternion.Euler(rotBalls);
        ballOne.localRotation = Quaternion.Euler(rotOne);
        ballTwo.localRotation = Quaternion.Euler(rotTwo);
        ballOne.SetParent(ballsParent);
        ballTwo.SetParent(ballsParent);
        Debug.Log("End coroutine");
        coroutineWorked = false;
    }
}
