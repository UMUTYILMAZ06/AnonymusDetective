using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PositionChangeTracker : MonoBehaviour
{
    private InputDevice leftHand;
    private InputDevice rightHand;
    private bool isGrabbing = false;
    private Vector3 initialPosition;
    private Vector3 finalPosition;
    [HideInInspector]
    public Vector3 positionChange;

    void Start()
    {
        // Get the left and right hand devices
        var leftHandDevices = new List<InputDevice>();
        var rightHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, leftHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, rightHandDevices);

        if (leftHandDevices.Count > 0) leftHand = leftHandDevices[0];
        if (rightHandDevices.Count > 0) rightHand = rightHandDevices[0];
    }

    void Update()
    {
        // Check for grab action (assuming a button press for grab)
        if (IsGrabbing(leftHand) || IsGrabbing(rightHand))
        {
            if (!isGrabbing)
            {
                isGrabbing = true;
                initialPosition = GetHandPosition();
                StartCoroutine(TrackHandMovement());
            }
        }
        else
        {
            isGrabbing = false;
        }
    }

    private bool IsGrabbing(InputDevice hand)
    {
        // Replace with the appropriate input check for your setup
        bool isGrabbing;
        hand.TryGetFeatureValue(CommonUsages.gripButton, out isGrabbing);
        return isGrabbing;
    }

    private Vector3 GetHandPosition()
    {
        Vector3 position;
        if (leftHand.TryGetFeatureValue(CommonUsages.devicePosition, out position) && IsGrabbing(leftHand))
        {
            return position;
        }
        else if (rightHand.TryGetFeatureValue(CommonUsages.devicePosition, out position) && IsGrabbing(rightHand))
        {
            return position;
        }
        return Vector3.zero;
    }

    private IEnumerator TrackHandMovement()
    {
        yield return new WaitForSeconds(0.8f);
        finalPosition = GetHandPosition();
        positionChange = finalPosition - initialPosition;
        Debug.Log($"Hand moved by: {positionChange} over 1 second.");
    }
}
