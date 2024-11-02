using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    [SerializeField] private int _playerStatusIndexRequired;
    [SerializeField] private int _moneyMultiplier;
    [SerializeField] private List<Transform> _doors;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private TextMeshProUGUI _multiplierTMP;
    private bool _isRotatingDoors = false;
    private void Start()
    {
        SetMultiplerText();
    }
    private void SetMultiplerText()
    {
        _multiplierTMP.text = "X" + _moneyMultiplier.ToString();
    }
    private void Update()
    {
        RotateDoors();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            if(PlayerData.CurrentPlayerStatusIndex >= _playerStatusIndexRequired)
            {
                _isRotatingDoors = true;
                PlayerData.OnMultiplierSet.Invoke(_moneyMultiplier);
            }
            else
            {
                GameManager.OnGameOver.Invoke(true);
            }
        }
    }
    private void RotateDoors()
    {
        if(_isRotatingDoors)
        {
            Vector3 _rotationVector = _rotationSpeed * Time.deltaTime * new Vector3(0f, 1f, 0f);
            _doors[0].Rotate(_rotationVector);
            _doors[1].Rotate(-_rotationVector);
            if (_doors[0].localRotation.eulerAngles.y>=90f)
            {
                _isRotatingDoors = false;
            }
        }
    }
}
