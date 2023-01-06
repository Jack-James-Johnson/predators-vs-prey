using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject toHide;
    public GameObject toShow;
    public void switchActiveMenu()
    {
        toHide.SetActive(false);
        toShow.SetActive(true);
    }
}
