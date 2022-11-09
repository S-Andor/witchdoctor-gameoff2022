using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour, ICollectible
{
    #region Properties
    public CollectibleSO CollectibleSO;
    public BoxCollider mCollider;

    public bool InUI { get ; set ; }
    public int ID { get; set; }
    CollectibleSO ICollectible.CollectibleSO { get => CollectibleSO; set => CollectibleSO = value; }
   
    private Rigidbody mRigidbody;
    #endregion

    #region Item interactions
    public ICollectible TakeItem()
    {
        var lItem = this;
        gameObject.SetActive(false);
        
        return lItem;
    }
    public void DropItem(Vector3 pSpawnLocation)
    {
        gameObject.transform.position = pSpawnLocation;
        gameObject.SetActive(true);
        mCollider.enabled = true;
        mRigidbody.constraints = RigidbodyConstraints.None;
    }
    #endregion Item interactions

    #region Monobehvaior
    // Start is called before the first frame update
    void Start()
    {
        InUI = false;
        mRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(transform.position.y < -100)
        {
            mRigidbody.velocity = new Vector3();
            gameObject.transform.position = new Vector3(transform.position.x, 4.0f, transform.position.z);
        }
    }
    #endregion Monobehvaior
}
