using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region Properties
    public LayerMask layerMask;
    public float rayDistance = 10.0f;
    public float rayRadius = 10.0f;

    private Rigidbody mRigidBody;
    private float mHitDistance;
    private NavMeshAgent mAgent;
    private bool mIsSearching = false;
    private Transform mTarget = null;
    #endregion Properties

    #region Monobehaviour stuff
    // Start is called before the first frame update
    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Looking();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagEnum.PLAYER) )
        {
            mAgent.destination = other.transform.transform.position;
            mTarget = other.transform;
            mIsSearching = true;
        }
    }
    
    #endregion Monobehaviour stuff

    #region Raycast
    void Looking()
    {

        RaycastHit lHit;
        Vector3 lOrigin = new Vector3(transform.transform.position.x, transform.position.y + 0.5f, transform.position.z);

        if (Physics.SphereCast(lOrigin, rayRadius, transform.forward, out lHit, rayDistance, layerMask,QueryTriggerInteraction.Ignore))
        {
            mHitDistance = lHit.distance;
            
            if(lHit.collider.CompareTag(TagEnum.PLAYER))
            {
                mAgent.destination = lHit.transform.transform.position;
                mTarget = lHit.transform;
            }
        }
        else
        {
            if(mTarget != null && mIsSearching == false)
            {
                mHitDistance = rayDistance;
                mAgent.destination = transform.position;
                mTarget = null;
            }
            
        }
    }
    #endregion Raycast

    #region Helper
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.transform.position , transform.transform.position * mHitDistance);
        Gizmos.DrawWireSphere(transform.transform.position + transform.forward * mHitDistance , rayRadius);
    }
    #endregion Helper
}
