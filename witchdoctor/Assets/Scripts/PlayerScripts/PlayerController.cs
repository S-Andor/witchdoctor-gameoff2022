using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region properties
    private Rigidbody mRigidBody;
    private PlayerUIManager mPlayerUIManager;
    private Inventory mInventory;
   
    public bool isGrounded;
    public float speed;
    public float gravity;
    public bool isSprinting;
    public float jumpHeight;
    public GameObject player;
    public float rayDistance = 3;
    public float rotationSpeed = 15;
    #endregion properties

    #region MonoBehaviour start/update/fixedUpdate etc.
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        mRigidBody = GetComponent<Rigidbody>();
        mPlayerUIManager = GetComponent<PlayerUIManager>();
        mInventory = GetComponent<Inventory>();
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
        DoRaycast();
    }
    #endregion MonoBehaviour start/update/fixedUpdate etc.

    #region Movement
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
    #endregion Movement

    #region Triggers & Collisions
    void OnTriggerEnter(Collider pOther)
    {
        isGrounded = pOther.CompareTag(TagEnum.GROUND);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(TagEnum.ENEMY))
        {
            mInventory.RemoveAllItems();
        }
    }
    #endregion Triggers

    #region Raycast
    void DoRaycast()
    {
        int lLayerMask = ~3;

        RaycastHit lHit;
        Vector3 lOrigin = new Vector3(transform.transform.position.x, transform.position.y + 0.5f, transform.position.z);

        if (Physics.Raycast(lOrigin,Camera.main.transform.forward, out lHit, rayDistance, lLayerMask))
        {
            Debug.DrawRay(lOrigin, Camera.main.transform.forward * lHit.distance, Color.red);
            HandleItemRaycast(lHit);
            return;
        }
        if(mPlayerUIManager.isPressEActive)
            mPlayerUIManager.ShowHidePressText(false);
    }

    void HandleItemRaycast(RaycastHit pHit)
    {
        if (pHit.collider.TryGetComponent<ICollectible>(out var lCollectible))
        {
            if (mPlayerUIManager.isPressEActive == false)
                mPlayerUIManager.ShowHidePressText(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(mInventory.MaxInventorySpace > mInventory.ItemCount)
                {
                    var lItem = lCollectible.TakeItem();
                    if (lItem != null)
                    {
                        mInventory.AddItem(lItem);
                    }
                }
                else
                {
                    Debug.Log("Inventory full");
                }

            }
        }
    }
    #endregion Raycast
}

