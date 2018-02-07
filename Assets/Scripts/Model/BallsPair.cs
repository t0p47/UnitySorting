using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsPair {

    private GameObject firstBall;
    private GameObject secondBall;

    public GameObject FirstBall
    {
        get
        {
            return firstBall;
        }

        set
        {
            firstBall = value;
        }
    }

    public GameObject SecondBall
    {
        get
        {
            return secondBall;
        }

        set
        {
            secondBall = value;
        }
    }
}
