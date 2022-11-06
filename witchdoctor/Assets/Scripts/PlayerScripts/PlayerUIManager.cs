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

    void Start()
    {
        ShowHidePressText(isPressEActive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHidePressText(bool pShow)
    {
        isPressEActive = pShow; 
        Debug.Log("called" + pShow);
        PressETxt.gameObject.SetActive(isPressEActive);
    }
}
