﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CanvasManager.IsStartGeme = true;
            CanvasManager.IsGameFlow = true;
            FacebookManager.Instance.LevelStart(PlayerPrefs.GetInt("Level"));

            gameObject.SetActive(false);
        }
    }
}
