// using System;
// using System.Collections;
// using System.Collections.Generic;

using Codes.Char;
using UnityEngine;

public class JoystickMovement : MonoBehaviour
{
    private Rigidbody _rigidbodyChar;
    private Vector3 _movement;
    private CharAnimation _charAnimation;
    [SerializeField] float speed;

    private void Start()
    {
        _charAnimation = GetComponent<CharAnimation>();
        _rigidbodyChar = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Began:
                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    _charAnimation.RunningOn();
                    _movement = Vector3.right * (Joystick.Instance.Horizontal * Time.deltaTime * speed) + Vector3.forward * (Joystick.Instance.Vertical * Time.deltaTime * speed);
                    _rigidbodyChar.Move(_rigidbodyChar.position + _movement, Quaternion.identity);
                    transform.rotation = Quaternion.Euler(0, Mathf.Atan2(Joystick.Instance.Horizontal, Joystick.Instance.Vertical) * 180 / Mathf.PI, 0);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _charAnimation.RunningOff();
                    break;
            }
            
        }
        
    }
}
