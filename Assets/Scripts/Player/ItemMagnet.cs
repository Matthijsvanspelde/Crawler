using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    [SerializeField] private float pickUpRadius;
    [SerializeField] private LayerMask CanHit;

    private Vector3 Target = Vector3.zero;

    private SphereCollider col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<SphereCollider>();
        col.radius = pickUpRadius;
        col.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != Vector3.zero)
        {
            //check for hit
            if (IsHit(Target))
            {
                //TODO: trigger item move to player

            }

            //reset vector
            Target = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Target = other.transform.position;
    }

    private bool IsHit(Vector3 targetPos)
    {
        Ray ray = new Ray(transform.position, targetPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickUpRadius, CanHit))
        {
            Debug.DrawRay(transform.position, targetPos, Color.yellow, 5f);
            return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
