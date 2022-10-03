using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;

    void Start()
    {
        Destroy(this.gameObject, 5.0f);
        //transform.LookAt(new Vector3(PlayerController.Instance.GetTargetPoint().x, transform.position.y, PlayerController.Instance.GetTargetPoint().z));
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);     
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Health>() != null)
        {
            other.gameObject.GetComponent<Health>().Damage(damage);
            
        }

        if(!other.isTrigger)
            Destroy(this.gameObject);
    }
}
