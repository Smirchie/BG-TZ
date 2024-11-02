using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 90f;
    private bool _isTurning;
    private float _turnAngle;
    private float _rotationDelta;
    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case ("RightTurn"):
                {
                    StartTurning(90f);
                    Destroy(collider.gameObject);
                    break;
                }
            case ("LeftTurn"):
                {
                    StartTurning(-90f);
                    Destroy(collider.gameObject);
                    break;
                }
        }
    }
    private void StartTurning(float _angle)
    {
        _turnAngle = _angle;
        _rotationDelta = 0f;
        _isTurning = true;
        PlayerMovement.IsMoving = false;
    }
    private void Turn()
    {
        if(_isTurning)
        {
            Vector3 rotationVector = _turnSpeed * Time.deltaTime * new Vector3(0f, 1f, 0f);
            if (_turnAngle < 0)
            {
                rotationVector *= -1;
            }
            transform.parent.Rotate(rotationVector);
            _rotationDelta += Mathf.Abs(rotationVector.y);
            if(_rotationDelta>=90f)
            {

                transform.parent.rotation = Quaternion.Euler(0f,RoundTo(transform.parent.rotation.eulerAngles.y,90), 0f);
                _isTurning = false;
                PlayerMovement.IsMoving = true;
            }
        }
    }
    private static int RoundTo(float value, int roundTo)
    {
        return (int)Mathf.Round(value / roundTo) * roundTo;
    }
    private void Update()
    {
        Turn();
    }
}