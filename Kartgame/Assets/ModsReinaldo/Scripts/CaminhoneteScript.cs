using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaminhoneteScript : ChangeColliders
{
    void Start()
    {
        ChangeWheelPosition(FrontLeft, FrontRight, RearLeft, RearRight);
        ChangeCentreOfMass(carMass);
        ChangePlayer(playerSpot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
