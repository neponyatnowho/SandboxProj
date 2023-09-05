using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CarSuspension : MonoBehaviourImproved
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _forceAmount;
    [SerializeField] private float _suspensionLength;

    [SerializeField] private Transform[] _suspensionTargets;
    [SerializeField] private Transform _centerOfMass;

    private void Start()
    {
        //_rigidbody.centerOfMass = _centerOfMass.position;
    }
    //private void FixedUpdate()
    //{

    //    for (int i = 0; i < _suspensionTargets.Length; i++)
    //    {
    //        Vector3 suspensionStart = _suspensionTargets[i].position;
    //        Vector3 suspensionEnd = _suspensionTargets[i].position - Vector3.up * _suspensionLength;

    //        bool cast = Physics.Linecast(suspensionStart, suspensionEnd);

    //        if (cast)
    //            _rigidbody.AddForce(_suspensionTargets[i].transform.position + (Vector3.up * _forceAmount), ForceMode.Force);

    //        Color rayColor = cast ? Color.green : Color.red;
    //        Debug.DrawLine(suspensionStart, suspensionEnd, rayColor);
    //    }
    //}

    private void FixedUpdate()
    {
        for (int i = 0; i < _suspensionTargets.Length; i++)
        {
            Vector3 suspensionStart = _suspensionTargets[i].position;
            Vector3 suspensionEnd = _suspensionTargets[i].position - Vector3.up * _suspensionLength;

            RaycastHit hit;

            if (Physics.Raycast(suspensionStart, -Vector3.up, out hit, _suspensionLength))
            {
                float distance = Vector3.Distance(suspensionStart, hit.point);

                float modifier = 1f - (distance / _suspensionLength);

                Vector3 forceDirection = _suspensionTargets[i].transform.position + (Vector3.up * _forceAmount * modifier) - _rigidbody.position;
                //_rigidbody.AddForceAtPosition(forceDirection.normalized * _forceAmount, _suspensionTargets[i].position, ForceMode.Force);
                _rigidbody.AddForce(forceDirection, ForceMode.Force);
            }

            Color rayColor = hit.collider ? Color.green : Color.red;
            Debug.DrawLine(suspensionStart, hit.point, rayColor);
        }
    }
}
