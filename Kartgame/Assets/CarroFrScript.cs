using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarroFrScript : ChangeColliders
{
    void Start()
    {
        ChangeWheelPosition(FrontLeft, FrontRight, RearLeft, RearRight);
        ChangeCentreOfMass(carMass);
        ChangePlayer(playerSpot);

    }
}
