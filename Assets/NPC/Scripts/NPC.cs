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

    void Update()
    {
        SearchPlayer();
        Attack();
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

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                Target = collider.GetComponent<Transform>();
                Vector3 direction = collider.transform.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(direction.y, 0, direction.z)), 0.1f);
                transform.Translate(new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime);
                //rb.MovePosition(direction * speed * Time.deltaTime);

                Animator.SetBool("isWalking", true);
            }
        }
    }


    public void StopSearchPlayer() => Animator.SetBool("isWalking", false);
}
