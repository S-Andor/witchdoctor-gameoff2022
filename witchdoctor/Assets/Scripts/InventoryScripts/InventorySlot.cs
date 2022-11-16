using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    #region Events
    public delegate void ItemsDropped(List<int> pItemID);
    public event ItemsDropped OnItemDropped;
    #endregion Events

    #region properties
    private List<ICollectible> mCollectibleItems  = new List<ICollectible>();

    public ICollectible ItemType { get; private set; }
    public int Count { get { return mCollectibleItems.Count; }}
    public int MaxCount { get => 8; }

    [SerializeField]
    public GameObject Slot;
    public GameObject TxtBackground;
    public TMP_Text ItemName;
    public GameObject ItemCount;
    public GameObject Button;
    public Image Image;
    #endregion properties

    #region MonoBehaviour
    #endregion MonoBehaviour

    #region Add/Remove Items
    public void AddItem(ICollectible pItem)
    {
        TxtBackground.SetActive(true);
        ItemCount.SetActive(true);
        Button.SetActive(true);

        ItemType = pItem;
        mCollectibleItems.Add(pItem);

        Image.sprite = pItem.CollectibleSO.Sprite;

        ItemName.SetText(pItem.CollectibleSO.Name);
        ItemCount.GetComponent<TMP_Text>().SetText(Count.ToString());

        Image.enabled = true;

    }
    public void DropItems()
    {
        List<int> lIDs = new List<int>();
        mCollectibleItems.ForEach(i => lIDs.Add(i.ID));
        OnItemDropped(lIDs);

        ResetSlot();
    }

    #endregion Add/Remove Items

    #region Helpers
    public void IncreaseCount(ICollectible pItem)
    {
        mCollectibleItems.Add( pItem);
        ItemCount.GetComponent<TMP_Text>().SetText(Count.ToString());
    }

    public void ResetSlot()
    {

        TxtBackground.SetActive(false);
        ItemCount.SetActive(false);
        Button.SetActive(false);

        ItemType = null;

        mCollectibleItems = new List<ICollectible>();
        Image.sprite = null;

        ItemName.SetText("");
        ItemCount.GetComponent<TMP_Text>().SetText("");

        Image.enabled = false;
    }
    #endregion Helpers
}
