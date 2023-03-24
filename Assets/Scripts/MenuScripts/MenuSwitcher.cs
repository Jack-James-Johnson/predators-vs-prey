using UnityEngine;
public class MenuSwitcher : MonoBehaviour
{
    public GameObject toHide;
    public GameObject toShow;
    public void switchActiveMenu()
    {
        //disables the variable that is set to toHide, and enables the variable set to ToShow
        toHide.SetActive(false);
        toShow.SetActive(true);
    }
}