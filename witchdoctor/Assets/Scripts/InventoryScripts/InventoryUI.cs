using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    #region Properties
    public Inventory Inventory;
    public GameObject InventoryUIElement;

    private InventorySlot[] mInventorySlots;
    #endregion Properties

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        Inventory.OnInventoryChange += UpdateUI;

        mInventorySlots = GetComponentsInChildren<InventorySlot>();

        foreach (InventorySlot iSlot in mInventorySlots)
        {
            iSlot.OnItemDropped += RemoveItems;
        }
        InventoryUIElement.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryUIElement.SetActive(!InventoryUIElement.active);
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.Confined : CursorLockMode.Locked;
        }
       
    }
    #endregion MonoBehaviour

    #region UI update
    void UpdateUI(ICollectible pItem)
    {
        for(int i = 0; i < Inventory.Items.Count; i++)
        {
            if (Inventory.GetItem(i).InUI == false && Inventory.GetItem(i) != null)
            {
                foreach (InventorySlot iSlot in mInventorySlots)
                {
                    if (iSlot.Count == 0)
                    {
                        iSlot.AddItem(Inventory.GetItem(i));
                        Inventory.ItemInUI(i);
                        break;
                    }

                    if (iSlot.Count < iSlot.MaxCount && iSlot.ItemType.CollectibleSO.Name.Equals(Inventory.GetItem(i).CollectibleSO.Name))
                    {
                        Inventory.ItemInUI(i);
                        iSlot.IncreaseCount(Inventory.GetItem(i));
                        break;

                    }
                }


            }

        }

    }
    #endregion UI Update

    #region "Event handling"
    void RemoveItems(List<int> pIDs)
    {
        Inventory.RemoveItems(pIDs);
    }
    #endregion "Event handling"

}
