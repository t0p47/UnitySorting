using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AbstractShareSort : MonoBehaviour {

    protected IEnumerator

    public Transform arrayPlacesParent;
    public Transform ballsParent;

    protected int[] array;
    protected int length;
    protected List<BallsPair> ballsPairList = new List<BallsPair>();

    [HideInInspector] public List<GameObject> arrayPlaces = new List<GameObject>();
    [HideInInspector] public List<GameObject> arrayBalls = new List<GameObject>();
    [HideInInspector] public System.Random _random = new System.Random();

    [HideInInspector] public int[] tmpIntArr;

    public Transform centerObject;
    private int animationCounter = 0;
    private int maxAnimCounter = 0;

    public bool coroutineWorked = false;

    public virtual void Start()
    {
        centerObject = Instantiate(centerObject);

        for (int i = 0; i < ballsParent.childCount; i++)
        {
            arrayBalls.Add(ballsParent.GetChild(i).gameObject);
            //Debug.Log("Ball name "+ballsParent.GetChild(i).name);
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

        Debug.Log(arrayBalls[0].name);

        resetBallArray();
        Debug.Log(arrayBalls[0].name);
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetBallArray();
        }
    }

    public void resetBallArray()
    {
        tmpIntArr = Shuffle(tmpIntArr);

        for (int i = 0; i < arrayPlaces.Count; i++)
        {
            Debug.Log("Shuffled TmpIntArray: index " + i + ", value " + tmpIntArr[i]);
            arrayBalls[tmpIntArr[i]].transform.position = arrayPlaces[i].transform.position;
        }
    }

    /*int[] Shuffle(int[] array)
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
    }*/

    int[] Shuffle(int[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(0, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
        return array;
    }

    public virtual void exchangeNumbers(int i, int j)
    {

        if (i != j)
        {
            BallsPair ballPair = new BallsPair();
            ballPair.FirstBall = arrayBalls[array[i]];
            ballPair.SecondBall = arrayBalls[array[j]];
            ballsPairList.Add(ballPair);
        }

        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    public void playBallsAnimation(List<BallsPair> ballsPairList)
    {
        animationCounter = 0;
        maxAnimCounter = ballsPairList.Count;

        /*Vector3 center = Vector3.Lerp(ballOne.position, ballTwo.position, 0.5f);

        centerObject.position = center;
        ballOne.SetParent(centerObject);
        ballTwo.SetParent(centerObject);*/

        float speed = 2f;

        StartCoroutine(rotateAroundCenterNew(ballsPairList, speed));

    }

    public IEnumerator rotateAroundCenterNew(List<BallsPair> ballsPairList, float overTime)
    {

        Transform ballOne = ballsPairList[animationCounter].FirstBall.transform;
        Transform ballTwo = ballsPairList[animationCounter].SecondBall.transform;

        Vector3 center = Vector3.Lerp(ballOne.position, ballTwo.position, 0.5f);

        centerObject.position = center;
        ballOne.SetParent(centerObject);
        ballTwo.SetParent(centerObject);

        float startTime = Time.time;

        Vector3 rotBalls = centerObject.rotation.eulerAngles;
        rotBalls = new Vector3(0, 0, rotBalls.z + 180);

        Vector3 rotOne = ballOne.localRotation.eulerAngles;
        rotOne = new Vector3(0, 0, rotOne.z - 180);

        Vector3 rotTwo = ballTwo.localRotation.eulerAngles;
        rotTwo = new Vector3(0, 0, rotTwo.z - 180);

        while (Time.time < startTime + overTime)
        {
            centerObject.rotation = Quaternion.Slerp(centerObject.rotation, Quaternion.Euler(rotBalls), (Time.time - startTime) / overTime);

            ballOne.localRotation = Quaternion.Slerp(ballOne.localRotation, Quaternion.Euler(rotOne), (Time.time - startTime) / overTime);

            ballTwo.localRotation = Quaternion.Slerp(ballTwo.localRotation, Quaternion.Euler(rotTwo), (Time.time - startTime) / overTime);
            yield return null;
        }

        centerObject.rotation = Quaternion.Euler(rotBalls);
        ballOne.localRotation = Quaternion.Euler(rotOne);
        ballTwo.localRotation = Quaternion.Euler(rotTwo);
        ballOne.SetParent(ballsParent);
        ballTwo.SetParent(ballsParent);

        if (animationCounter != maxAnimCounter - 1)
        {
            animationCounter++;
            StartCoroutine(rotateAroundCenterNew(ballsPairList, overTime));
        }

    }
}
