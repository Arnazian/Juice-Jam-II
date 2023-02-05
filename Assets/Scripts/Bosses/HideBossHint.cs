using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBossHint : MonoBehaviour
{
    public void DoHideBossHint()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        PauseMenu.Instance.IsPaused = false;
    }
}
