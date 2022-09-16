using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitController
{
    public static PlayerController Instance;

    //Player variables
    public Camera playerCamera;
    public Gun gunController;
    float moveSpeed = 5.0f;
    Rigidbody rb;

    Vector3 moveDirection = new Vector3();
    Vector3 cursorPosition = new Vector3();
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Instance = this;

        GetComponent<Health>().OnDamage += OnHit;
    }
    

    void Start()
    {
        
    }

    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            moveSpeed = 10f;
        else
            moveSpeed = 5f;

        moveDirection.x = x * moveSpeed;
        moveDirection.z = z * moveSpeed;

        Ray casting = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        float rayLenght;
        if(groundPlane.Raycast(casting, out rayLenght))
        {
            Vector3 lookPoint = casting.GetPoint(rayLenght);
            cursorPosition.x = lookPoint.x;
            cursorPosition.y = transform.position.y;
            cursorPosition.z = lookPoint.z;
        }

        if(Input.GetMouseButtonDown(0))
        {
            gunController.isFiring = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            gunController.isFiring = false;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection;
        transform.LookAt(cursorPosition);
    }


    void LateUpdate()
    {
        playerCamera.transform.position = new Vector3(transform.position.x, playerCamera.transform.position.y, transform.position.z);
        //playerCamera.transform.position.z = transform.position.z;
    }

}
