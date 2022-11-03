using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region properties
    private Rigidbody mRigidBody;
   
    public bool isGrounded;
    public float speed;
    public float gravity;
    public bool isSprinting;
    public float jumpHeight;
    public GameObject player;

    public float rotationSpeed = 15;
    #endregion
    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();     
    }

    private void Update()
    {
        Jump();
        Rotation();
    }
    void Movement()
    {
        Vector3 lMoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        lMoveDirection = Camera.main.transform.TransformDirection(lMoveDirection);

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            lMoveDirection *= 10.0f;
        }
        else
        {
            lMoveDirection *= speed;
        }
        lMoveDirection.y = 0;

        mRigidBody.MovePosition(mRigidBody.position + lMoveDirection * Time.fixedDeltaTime);
    }

    void Jump()
    {

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            mRigidBody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            isGrounded = false;
        }
    }

    void Rotation()
    {
        float y = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
        player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y + y, 0);
    }


    void OnTriggerEnter(Collider other)
    {
        isGrounded = other.CompareTag("Ground");
    }
}

