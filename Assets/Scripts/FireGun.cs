using UnityEngine;

public class FireGun : MonoBehaviour
{
    public float damageTime = 0.02F;
    public float range = 100f;
    public Camera thirdPersonCam;
    public LayerMask ignoreLayerMask;
    public Animator animator;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    Transform target;
    void Shoot()
    {
        RaycastHit hit;
        Ray ray = thirdPersonCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, range, ~ignoreLayerMask))
        {
            if (hit.transform.tag != "NPC") return;
            target = hit.transform;
            animator.SetTrigger("Attack");
        }
    }

    public void Fire()
    {

        if (target != null)
        {
            Destroy(target.gameObject);
            target = null;
        }

        // Çarpma efekti oluþtur
        //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //Destroy(impactGO, 2f); // 2 saniye sonra çarpma efektini yok et
    }
}
