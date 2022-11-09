using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectible
{
    public int ID { get; set; }
    public CollectibleSO CollectibleSO { get; set; }
    public void DropItem(Vector3 pSpawnLocation);
    public ICollectible TakeItem();
    public bool InUI { get; set; }
    
}
