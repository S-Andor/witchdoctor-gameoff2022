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
    private Image mImage;

    public ICollectible ItemType { get; private set; }
    public int Count { get; private set; }
    public int MaxCount { get => 8; }

    [SerializeField]
    public GameObject Slot;
    public GameObject TxtBackground;
    public TMP_Text ItemName;
    public GameObject ItemCount;
    public GameObject Button;
    #endregion properties

    #region MonoBehaviour
    private void Start()
    {
        mImage = Slot.GetComponent<Image>();
        Count = 0;

    }
    #endregion MonoBehaviour

    #region Add/Remove Items
    public void AddItem(ICollectible pItem)
    {
        Count++;

        TxtBackground.SetActive(true);
        ItemCount.SetActive(true);
        Button.SetActive(true);

        ItemType = pItem;
        mCollectibleItems.Add(pItem);

        mImage.sprite = pItem.CollectibleSO.Sprite;

        ItemName.SetText(pItem.CollectibleSO.Name);
        ItemCount.GetComponent<TMP_Text>().SetText(Count.ToString());

        mImage.enabled = true;

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
        Count++;
        mCollectibleItems.Add( pItem);
        ItemCount.GetComponent<TMP_Text>().SetText(Count.ToString());
    }

    void ResetSlot()
    {

        TxtBackground.SetActive(false);
        ItemCount.SetActive(false);
        Button.SetActive(false);

        ItemType = null;

        Count = 0;
        mImage.sprite = null;

        ItemName.SetText("");
        ItemCount.GetComponent<TMP_Text>().SetText("");

        mImage.enabled = false;
    }
    #endregion Helpers
}
