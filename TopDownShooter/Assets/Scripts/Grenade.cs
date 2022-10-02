using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    float explodeTime;
    [SerializeField]
    float maxRadius;

    void Update()
    {
        if (explodeTime > 0)
        {
            explodeTime -= 1f * Time.deltaTime;
            if (explodeTime <= 0)
                Explosion();
        }
    }

    void Explosion()
    {
        explodeTime = float.MaxValue;

        //disable collisions
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
            collider.enabled = false;

        GetComponent<AudioSource>().Play();

        GetComponent<MeshRenderer>().enabled = false;

        Destroy(this.gameObject, 3.0f);

        //explosion hits
        RaycastHit[] hits = Physics.SphereCastAll(transform.position + new Vector3(0,0.5f,0), maxRadius, Vector3.up);
        foreach(RaycastHit hit in hits)
        {
            if (hit.transform.tag == "Grenade")
                continue;

            GameObject obj = hit.transform.gameObject;
            float distanceFactor = 1f - Mathf.Clamp(Vector3.Distance(transform.position, obj.transform.position), 0, maxRadius) / maxRadius;


            if (obj.GetComponent<Health>() != null)
            {
                Vector3 half = new Vector3(0,0.5f,0);
                Ray rayHit = new Ray(transform.position, (obj.transform.position + half) - transform.position);
                RaycastHit hitResult;
                if (Physics.Raycast(rayHit, out hitResult, maxRadius))
                {
                    if (hitResult.collider.gameObject == obj)
                    {
                        obj.GetComponent<Health>().Damage(damage * distanceFactor);
                    }
                }
            }

            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = obj.transform.position - transform.position;
                
                Vector3 addForce = direction * (50f) * distanceFactor;
                Vector3 upForce = Vector3.up * 10f * distanceFactor;
                rb.AddForce(addForce + upForce, ForceMode.Impulse);
            }
        }
    }

    
    
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            Explosion();

            Destroy(this.gameObject, 3.0f);
        }
    }
    */
}
