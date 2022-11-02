using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region properties
    private Rigidbody mRigidBody;
    //private CharacterController mPlayerController;
    private Vector3 mPlayerVelocity;
   
    public bool isGrounded;
    public float speed;
    public float gravity;
    public bool isSprinting;
    public float jumpHeight;
    public GameObject player;

    private Vector3 mMousePositionViewport = Vector3.zero;
    private Quaternion mDesiredRotation = new Quaternion();
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
        Rotation();
    }

    private void Update()
    {
        Jump();
    }
    void Movement()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection *= 10.0f;
        }
        else
        {
            moveDirection *= speed;
        }
        moveDirection.y = 0;

        mRigidBody.MovePosition(mRigidBody.position + moveDirection * Time.fixedDeltaTime);
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
        float y = Input.GetAxis("Mouse X") * rotationSpeed;
        player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y + y, 0);
    }


    void OnTriggerEnter(Collider other)
    {
        isGrounded = other.tag == "Ground";
    }
}

