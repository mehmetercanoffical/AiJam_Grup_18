using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireGun : MonoBehaviour
{
    public float range = 100f; // Menzil
    public float damage = 10f; // Hasar
    public Camera thirdPersonCam; // ���nc� �ah�s Kamera
    //public GameObject impactEffect; // �arpma efekti
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
            target = hit.transform;
            animator.SetTrigger("Attack");
        }
    }

    public void Fire()
    {

        if (target != null)
        {
            //Target targetS = hit.transform.GetComponent<Target>();
            Debug.Log("Hit " + target.gameObject.name);
            //targetS.TakeDamage(damage);
        }

        // �arpma efekti olu�tur
        //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //Destroy(impactGO, 2f); // 2 saniye sonra �arpma efektini yok et
    }
}
