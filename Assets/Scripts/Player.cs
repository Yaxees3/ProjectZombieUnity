using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Camera")]
    public float lookSensitivity;
    public float maxLookX;
    public float minLookX;
    private float rotX;

    private Camera cam;
    private Rigidbody rig;


    private void Awake()
    {
        cam = Camera.main;
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }
        Cameralook();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 dir = transform.right * x + transform.forward * z;
        dir.y = rig.velocity.y;
        rig.velocity = dir;
        
    }

    void Cameralook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);

        cam.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
        transform.eulerAngles += Vector3.up * y;
    }


    void TryJump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray,1.1f))
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }


}

