using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StackMovementController : MonoBehaviour
{
    #region VARS
    [SerializeField] float touchSpeedRate = 0.01f;
    [SerializeField] float KeyboardSpeedRate = 8f;
    [SerializeField] float characterMoveSpeed = 5f;
    [SerializeField] Stickman firstStickman; 

    Touch touch;
    #endregion
    #region ENGINE
    // Update is called once per frame
    private void LateUpdate()
    {
        //KeyboardMove();
        TouchAndDragControl();
        EndlessForwardMovement();
    }
    void Update()
    {
        
        CameraFollow();
    }
   
    #endregion
    #region MEMBER METHODS
    void KeyboardMove()
    {
        float xInput = Input.GetAxis("Horizontal");
        if (xInput != 0)
        {
            transform.position = new Vector3
                  (
                      transform.position.x + xInput * KeyboardSpeedRate * Time.deltaTime,
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
                        transform.position.x + touch.deltaPosition.x * touchSpeedRate * Time.deltaTime,
                        transform.position.y,
                        transform.position.z

                    );
            }
        }
    }
    void EndlessForwardMovement()
    {
            transform.position = transform.position + Vector3.forward * characterMoveSpeed * Time.deltaTime;
    }
    void CameraFollow()
    {
        if (Camera.main)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z- 15f);
        }
    }
    #endregion

}
