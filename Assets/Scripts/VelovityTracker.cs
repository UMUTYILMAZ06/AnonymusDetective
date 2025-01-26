using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VelocityTracker : MonoBehaviour
{
    public int maxMember;
    // public float cameraOffset; // Remove this line
    //public Transform player;
    private Rigidbody rb;

    private Queue<Vector3> posqueue;

    public float forceMultyiplier;
    private XRGrabInteractable _interactable;

    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private Transform hand;
    private IEnumerator _startCalculate;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        posqueue = new Queue<Vector3>();

        _interactable = GetComponent<XRGrabInteractable>();

        _interactable.selectEntered.AddListener(Entered);
        _interactable.selectExited.AddListener(Exited);
    }

    public void Entered(SelectEnterEventArgs args)
    {
       
        hand = args.interactorObject.transform;
        Debug.Log("Enter");
        if (_startCalculate != null)
        {
            StopCoroutine(_startCalculate);
        }

        _startCalculate = StartToCalculate();

        StartCoroutine(_startCalculate);
    }

    void Exited(SelectExitEventArgs args)
    {
        initialPosition = posqueue.Peek();
        finalPosition = GetHandPosition();
        Vector3 headPosition = GetHeadPosition(); // Get the headset position

        Vector3 positionChange = finalPosition - initialPosition;

        //Debug.Log("Hand" + initialPosition.y);
        //Debug.Log("Head" + headPosition.y);

        if (initialPosition.y < headPosition.y - 0.4f) // Use headset position instead of cameraOffset
        {
            positionChange.x = 0;
            positionChange.z = 0;

            if (positionChange.y > 0.4f)
            {
                Debug.Log("Force");
                ApplyUpwardForce(positionChange);
            }
        }

        Debug.Log("Exit");
    }

    private Vector3 GetHandPosition()
    {
        return hand.position;
    }

    private Vector3 GetHeadPosition()
    {
        // Assuming the main camera represents the headset
        return Camera.main.transform.position;
    }

    private IEnumerator StartToCalculate()
    {
        while (true)
        {
            initialPosition = GetHandPosition();
            if (posqueue.Count >= maxMember)
            {
                Vector3 dequeuedPosition = posqueue.Peek();
                posqueue.Dequeue();
                // Debug.Log(dequeuedPosition);
            }

            posqueue.Enqueue(initialPosition);

            yield return new WaitForSeconds(0.7f);
        }
    }

    public void ApplyUpwardForce(Vector3 uforce)
    {
        // Use the position change from PositionChangeTracker and multiply it by the force multiplier
        Vector3 force = uforce * forceMultyiplier;
        rb.AddForce(force);
        //Debug.Log($"Hand moved by:" + force);
    }
}