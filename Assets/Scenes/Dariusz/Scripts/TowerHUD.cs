using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHUD : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Camera activeCamera;
    [SerializeField] private GameObject canvas;
    public static TowerHUD Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this;  }
        else { Destroy(gameObject); }
    }

    private void Update()
    {
        SetHUDActive();
    }
    
    public bool GetButtonStatus()
    {
        return button.IsActive();
    }

    private void SetHUDActive()
    {
        switch (activeCamera.isActiveAndEnabled)
        {
            case true:
                canvas.SetActive(true);
                break;
            case false:
                canvas.SetActive(false);
                break;
        }

    }
}
