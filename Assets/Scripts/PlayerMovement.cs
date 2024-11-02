using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _playerCollider;
    [SerializeField] private float _sideMovementRange;
    public static bool IsMoving { get; set; } = true;

    private void Start()
    {
        transform.forward = new Vector3(0f, 0f, 1f);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && IsMoving)
        {
            MoveSideways();
            MoveForward();
        }
    }

    private void MoveSideways()
    {
        _playerCollider.transform.localPosition = _sideMovementRange * TouchXAxis() * new Vector3(1f,0f,0f);
    }

    private void MoveForward()
    {
        transform.position += _speed * Time.deltaTime * transform.forward;
    }

    private float TouchXAxis()
    {
        Vector3 touchScreenPosition = Input.GetTouch(0).position;
        float _x = touchScreenPosition.x - Screen.width / 2;
        return _x / Screen.width;
    }
}