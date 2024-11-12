using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBullet : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, 0f, 10f));
    }
}
