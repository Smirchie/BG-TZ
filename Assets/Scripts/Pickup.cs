using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private int _moneyDelta;

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerData.OnItemPickUp.Invoke(_moneyDelta);
            Destroy(gameObject);
        }
    }
}