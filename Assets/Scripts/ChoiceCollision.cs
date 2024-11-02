using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceCollision : Pickup
{
    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            base.OnTriggerEnter(collider);
            Destroy(transform.parent.parent.gameObject);
        }
    }
}