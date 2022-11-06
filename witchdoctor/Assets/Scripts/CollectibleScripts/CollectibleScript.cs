using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour, ICollectible
{
    #region Properties
    public CollectibleSO CollectibleSO;

    #endregion
    public CollectibleSO TakeItem()
    {
        Debug.Log("Take this");
        Destroy(gameObject);
        return CollectibleSO;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
