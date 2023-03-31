using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColliders : MonoBehaviour
{
    public Collider carCollider;
    public Transform playerVisual, playerSpot, carMass;

    [SerializeField] private WheelCollider FrontLeftWheel;
    [SerializeField] private WheelCollider FrontRightWheel;
    [SerializeField] private WheelCollider RearLeftWheel;
    [SerializeField] private WheelCollider RearRightWheel;

    [SerializeField] protected Transform FrontLeft, FrontRight, RearLeft, RearRight;

    protected void ChangeWheelPosition(Transform FrontLeft, Transform FrontRight, Transform RearLeft, Transform RearRight)
    {
        FrontLeftWheel.transform.position = FrontLeft.transform.position;
        FrontRightWheel.transform.position = FrontRight.transform.position;
        RearLeftWheel.transform.position = RearLeft.transform.position;
        RearRightWheel.transform.position = RearRight.transform.position;
    }

    protected void ChangePlayer(Transform playerPosition)
    {
        playerVisual.position = playerPosition.position;
    }

    protected void ChangeCentreOfMass(Transform centerCollider)
    {
        carCollider.transform.position = centerCollider.position;
    }
}
