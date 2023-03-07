using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public void LateUpdate()
    {
        offset = target.position;
        offset.y = 83;
        this.transform.position=offset;
    }
}
