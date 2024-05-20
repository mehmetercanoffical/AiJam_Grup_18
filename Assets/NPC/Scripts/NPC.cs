using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public float speed;
    public float distance;
    public float distancePosition;

    public Animator Animator;
    public Transform Target;
    public Rigidbody rb;

    public bool isAttacking = false;
    public float rotationSpeed = .1f;

    void LateUpdate()
    {
        SearchPlayer();
        WalkToTarget();
        Attack();
    }

    public void WalkToTarget()
    {
        if (Target == null) return;

        Vector3 direction = Target.position - transform.position;
        direction.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), rotationSpeed);
        if (Vector3.Distance(transform.position, Target.position) > distancePosition)
        {
            Animator.SetBool("isWalking", true);
            Vector3 vector3 = new Vector3(direction.x, 0, direction.z);
            transform.Translate(vector3 * speed * Time.deltaTime);
        }
        else
            Animator.SetBool("isWalking", false);
    }

    public void Attack()
    {
        if (Target == null) return;
        if ((Vector3.Distance(transform.position, Target.position) < distancePosition))
        {
            if (isAttacking) return;
            StopSearchPlayer();
            Animator.SetTrigger("isAttacking");
            isAttacking = true;
        }
        else Target = null;
    }

    public void StopAttack()
    {
        isAttacking = false;
        Debug.Log("Stop Attack");
    }

    public void SearchPlayer()
    {
        if (Target != null) return;
        if (Target != null) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                Target = collider.GetComponent<Transform>();

            }
        }
    }


    public void StopSearchPlayer() => Animator.SetBool("isWalking", false);
}
