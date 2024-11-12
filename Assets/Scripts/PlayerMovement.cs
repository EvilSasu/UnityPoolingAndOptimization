using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float boundaryTop = 5f;
    public float boundaryBottom = -5f;
    public float movementSpeed = 10f;

    public Vector3 initialPosition;
    public float screenHeight;       

    private void Start()
    {
        initialPosition = transform.position;
        screenHeight = Screen.height;
    }

    private void Update()
    {
        float mouseY = Input.mousePosition.y / screenHeight * 2f - 1f; 
        float targetY = initialPosition.y + mouseY * movementSpeed;

        targetY = Mathf.Clamp(targetY, boundaryBottom, boundaryTop);

        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }
}
