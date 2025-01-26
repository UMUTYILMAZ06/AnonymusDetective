using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Transform tip;

    private Rigidbody _rigidBody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        PullInteraction.PullActionRelased += Release;

        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionRelased -= Release;
    }

    private void Release(float value)
    {
        PullInteraction.PullActionRelased -= Release;
        gameObject.transform.parent = null;
        _inAir = true;
        SetPhysics(true);

        //
        Vector3 scaledDirection = transform.forward * value * speed;
        Vector3 elementWiseMultiplier = new Vector3(1.7f, 1.7f, 1.7f);
        Vector3 force = new Vector3(
    scaledDirection.x * elementWiseMultiplier.x,
    scaledDirection.y * elementWiseMultiplier.y,
    scaledDirection.z * elementWiseMultiplier.z
    );
       // Vector3 force = transform.forward * value * speed ;
        _rigidBody.AddForce(force, ForceMode.Impulse);

         StartCoroutine(RotateWithVelocity());

        _lastPosition = tip.position;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while(_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidBody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    void FixedUpdate()
    {
        if(_inAir)
        {
            CheckCollision();
            _lastPosition = tip.position;
        }
    }

    private void CheckCollision()
    {
        if(Physics.Linecast(_lastPosition,tip.position, out RaycastHit hitInfo))
        {
            if(hitInfo.transform.gameObject.layer != 8)
            {
                if(hitInfo.transform.TryGetComponent(out Rigidbody body))
                {
                    _rigidBody.interpolation = RigidbodyInterpolation.None;
                    transform.parent = hitInfo.transform;
                    body.AddForce(_rigidBody.velocity, ForceMode.Impulse);
                }
                Stop();
            }
        }
    }

    private void Stop()
    {
        _inAir = false;
        SetPhysics(false);
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidBody.useGravity = usePhysics;
        _rigidBody.isKinematic = !usePhysics;
    }
}
