using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGameSpeed : MonoBehaviour
{
    public void ChangeSpeed(float val)
    {
        Time.timeScale = val;
    }
}
