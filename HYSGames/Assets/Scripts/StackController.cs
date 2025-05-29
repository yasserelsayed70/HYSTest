using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class StackController : MonoBehaviour
{
    #region VARS
    [SerializeField] float touchSpeedRate = 0.01f;
    [SerializeField] float characterMoveSpeed = 5f;

    Touch touch;
    Rigidbody rb;
    #endregion
    #region ENGINE
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
        //TouchAndDragControl();
        KeyboardMove();
        EndlessForwardMovement();
    }
    #endregion
    #region MEMBER METHODS
    void KeyboardMove()
    {
        float xInput = Input.GetAxis("Horizontal");
        if (xInput != 0)
        {
            //transform.position = new Vector3(xInput + transform.position.x * 0.2f, transform.position.y, transform.position.z);
            //rb.MovePosition(transform.position + new Vector3(2f, 0f, 0f) * xInput);
            transform.position = new Vector3
                  (
                      transform.position.x + xInput * touchSpeedRate,
                      transform.position.y,
                      transform.position.z

                  );
        }
    }
    void TouchAndDragControl()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3
                    (
                        transform.position.x + touch.deltaPosition.x * touchSpeedRate,
                        transform.position.y,
                        transform.position.z

                    );
            }
        }
    }
    void EndlessForwardMovement()
    {
        /*if (TryGetComponent<Rigidbody>( out rb))
        {
            //rb.MovePosition(transform.position + Vector3.forward * characterMoveSpeed);
        }*/
            transform.position = transform.position + Vector3.forward * characterMoveSpeed;
    }
    void CameraFollow()
    {
        if (Camera.main)
        {

            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z- 5f);
        }
    }
    #endregion

}
