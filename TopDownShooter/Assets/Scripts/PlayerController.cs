using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitController
{
    public static PlayerController Instance;

    //Player variables
    public Gun gunController;
    public Grenade grenadePrefab;
    float moveSpeed = 5.0f;
    Rigidbody rb;

    Vector3 moveDirection = new Vector3();
    Vector3 cursorPosition = new Vector3();
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    List<int> Keys = new List<int>();

    public void AddKey(int newkey)
    {
        Keys.Add(newkey);
    }

    public bool IsKey(int keyComp)
    {
        foreach(int key in Keys)
        {
            if (key == keyComp)
                return true;
        }

        return false;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Instance = this;

        GetComponent<Health>().OnDamage += OnHit;
    }
    
    void ThrowGrenade()
    {
        Grenade grenade = Instantiate(grenadePrefab, this.transform.position+(Vector3.up*2.5f), Quaternion.identity, this.transform.parent);

        Rigidbody rbGrenade = grenade.GetComponent<Rigidbody>();

        Vector3 addForce = transform.forward * 10f;

        rbGrenade.AddForce(addForce, ForceMode.Impulse);
    }
    
    void Update()
    {
        //get moving direction and speed
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            moveSpeed = 10f;
        else
            moveSpeed = 5f;

        moveDirection = moveDirection.normalized;

        //ger cursor position
        Ray casting = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        float rayLenght;
        if(groundPlane.Raycast(casting, out rayLenght))
        {
            Vector3 lookPoint = casting.GetPoint(rayLenght);
            cursorPosition.x = lookPoint.x;
            cursorPosition.y = transform.position.y;
            cursorPosition.z = lookPoint.z;
        }

        //get firing
        if(Input.GetMouseButtonDown(0))
        {
            gunController.isFiring = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            gunController.isFiring = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            ThrowGrenade();
        }
    }

    Vector3 targetVelocity;
    float maxVelocityChange = 3;

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;

        transform.LookAt(cursorPosition);
    }
}
