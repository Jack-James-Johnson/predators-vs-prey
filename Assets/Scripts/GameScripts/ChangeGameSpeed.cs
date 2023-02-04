using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGameSpeed : MonoBehaviour
{
    public void ChangeSpeed(float val)
    {
        // Change time scale according to the slider value
        Time.timeScale = val;
    }
}
