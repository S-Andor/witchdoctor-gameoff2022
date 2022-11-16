using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    #region Events
    public delegate void InventoryChange(InventoryChangeType pChangeType);
    public static event InventoryChange OnInventoryChange;
    #endregion Events

    #region Properties
    private List<ICollectible> mItems = new List<ICollectible>();
    private int mID = 1;

    public int MaxInventorySpace { get { return 128; } }
    public int ItemCount { get { return mItems.Count; } }
    public List<ICollectible> Items { get { return mItems; } }

    #endregion Properties

    #region Add/Remove
    public void AddItem(ICollectible pItem)
    {
        if (pItem.ID == 0)
            pItem.ID = mID++;

        mItems.Add(pItem);
        OnInventoryChange(InventoryChangeType.ADDED_ITEM);
    }

    public void RemoveItems(List<int> pItemIDs)
    {
        List<ICollectible> lDroppedItems = new List<ICollectible>();
        lDroppedItems = mItems.Where(x => pItemIDs.Contains(x.ID)).ToList();
        lDroppedItems.ForEach(i => {
            i.InUI = false;
            i.DropItem(gameObject.transform.position + transform.forward);
        });

        mItems = mItems.Where(x => !pItemIDs.Contains(x.ID)).ToList();
    }

    public void RemoveAllItems()
    {
        List<ICollectible> lDroppedItems = new List<ICollectible>();
        lDroppedItems = mItems;
        lDroppedItems.ForEach(i => {
            i.InUI = false;
            i.DropItem(gameObject.transform.position + transform.forward);
        });

        mItems = new List<ICollectible>();
        OnInventoryChange(InventoryChangeType.REMOVED_ALL);

    }
    #endregion Add/Remove

    #region GettingInfo
    public ICollectible GetItem(int index)
    {
        return mItems[index];
    }
    public void ItemInUI(int index)
    {
        mItems[index].InUI = true;
    }
    #endregion GettingInfo
}

