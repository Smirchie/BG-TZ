using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCollision : MonoBehaviour
{
    [SerializeField] private List<Transform> flags;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _rotationSpeed;
    private bool _isActivated = false;
    private bool _isRotatingFlags = false;

    private void Update()
    {
        RotateFlags();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && !_isActivated)
        {
            _isActivated = true;
            _isRotatingFlags = true;
            PlayerData.OnCheckPointActivate.Invoke(_spawnPoint);
        }
    }

    private void RotateFlags()
    {
        if (_isRotatingFlags)
        {
            Vector3 rotationVector = _rotationSpeed * Time.deltaTime * new Vector3(0f, 0f, -1f);
            foreach (var flag in flags)
            {
                flag.Rotate(rotationVector);
            }
            if (flags[0].rotation.z <= 0f)
            {
                _isRotatingFlags = false;
            }
        }
    }
}