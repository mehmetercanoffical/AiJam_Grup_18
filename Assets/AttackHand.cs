using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHand : MonoBehaviour
{
    public int damage = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TimePoints.Instance.DecreasePointsByJump(damage);

        }
    }
}
