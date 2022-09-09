using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour
{
    [SerializeField]
    Transform target;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            target.position.z
        ) + offset;
    }
}
