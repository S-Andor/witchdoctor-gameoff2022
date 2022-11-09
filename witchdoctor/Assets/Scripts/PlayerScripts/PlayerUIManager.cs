using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    #region properties
    public TMP_Text PressETxt;
    public bool isPressEActive = false;
    #endregion properties

    #region MonoBehaviour
    void Start()
    {
        ShowHidePressText(isPressEActive);
    }
    #endregion MonoBehaviour

    #region Press E UI
    public void ShowHidePressText(bool pShow)
    {
        isPressEActive = pShow; 
        PressETxt.gameObject.SetActive(isPressEActive);
    }
    #endregion Press E UI
}
