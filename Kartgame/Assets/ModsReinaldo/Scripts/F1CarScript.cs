using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F1CarScript : ChangeColliders
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeWheelPosition(FrontLeft, FrontRight, RearLeft, RearRight);
        ChangeCentreOfMass(carMass);
        ChangePlayer(playerSpot);
    }
}
