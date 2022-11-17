using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField] float smoothing;

    Vector3 offset;
    Vector3 cameraVelocity = Vector3.zero;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref cameraVelocity, smoothing);
    }
}
