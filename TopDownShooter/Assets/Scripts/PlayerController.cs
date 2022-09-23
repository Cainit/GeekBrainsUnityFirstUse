using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitController
{
    public static PlayerController Instance;


    //Player variables
    Animator animator;
    public Transform chestBone;
    public Gun gunController;
    public Grenade grenadePrefab;
    float moveSpeed = 5.0f;
    float speedBonus = 0f;

    Rigidbody rb;
    Vector3 moveDirection = new Vector3();
    Vector3 cursorPosition = new Vector3();
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    List<int> Keys = new List<int>();

    public Vector3 GetTargetPoint() { return cursorPosition; }

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
        animator = GetComponent<Animator>();
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

    public void AddBonusSpeed(float time)
    {
        speedBonus += time;
    }
    
    void Update()
    {
        //get moving direction and speed
        moveDirection.x = Input.GetAxis("Horizontal") * Time.deltaTime;
        moveDirection.z = Input.GetAxis("Vertical") * Time.deltaTime;

        //animator.SetFloat("Horizontal", Input.GetAxis("Horizontal") );
        //animator.SetFloat("Vertical", Input.GetAxis("Vertical"));

        moveSpeed = 5f;

        if (speedBonus > 0)
        {
            moveSpeed *= 2; 
            speedBonus -= 1.0f * Time.deltaTime;
        }

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

        AnimUpdate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        

    }

    void AnimUpdate(float h, float v)
    {
        Vector3 moveDir = new Vector3(h, 0, v);

        if (moveDir.magnitude > 1.0f)
        {
            moveDir = moveDir.normalized;
        }

        moveDir = transform.InverseTransformDirection(moveDir);

        animator.SetFloat("Horizontal", moveDir.x, 0.05f, Time.deltaTime);
        animator.SetFloat("Vertical", moveDir.z, 0.05f, Time.deltaTime);

        if (moveDir.z < 0)
            moveSpeed *= 0.5f;
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;

        transform.LookAt(cursorPosition);
    }

    void LateUpdate()
    {
        chestBone.LookAt(cursorPosition + Vector3.up);
        
        
        gunController.firePoint.LookAt(cursorPosition + Vector3.up);
    }
}
