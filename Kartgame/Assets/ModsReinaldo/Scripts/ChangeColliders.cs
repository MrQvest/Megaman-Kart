using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColliders : MonoBehaviour
{

    [SerializeField] private WheelCollider FrontLeftWheel;
    [SerializeField] private WheelCollider FrontRightWheel;
    [SerializeField] private WheelCollider RearLeftWheel;
    [SerializeField] private WheelCollider RearRightWheel;

    public static WheelCollider newFrontLeft;
    public static WheelCollider newFrontRight;
    public static WheelCollider newRearLeft;
    public static WheelCollider newRearRight;

    // Start is called before the first frame update
    void Start()
    {

      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
